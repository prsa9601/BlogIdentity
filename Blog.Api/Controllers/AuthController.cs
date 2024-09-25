using Blog.Api.Infrastructure.JwtUtil;
using Blog.Api.ViewModels.Auth;
using Blog.Application.User.AddToken;
using Blog.Application.User.ConfirmEmail;
using Blog.Application.User.ConfirmMobile;
using Blog.Application.User.GenerateTokenEmailConfirmed;
using Blog.Application.User.GenerateTokenMobileConfirmed;
using Blog.Application.User.Login;
using Blog.Application.User.Register;
using Blog.Application.User.RemoveToken;
using Blog.Application.User.SendEmail;
using Blog.Domain.UserAgg;
using Blog.Presentation.Facade.User;
using Blog.Query.User.DTOs;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace Blog.Api.Controllers;

public class AuthController : ApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;
    public AuthController(IUserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ApiResult> Login(LoginViewModel loginViewModel)
    {
        //<LoginResultDto?>
        //var user = await _userFacade.GetUserByPhoneNumber(loginViewModel.PhoneNumber);
        //if (user == null)
        //{
        //    var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
        //    return CommandResult(result);
        //}

        //  if (Sha256Hasher.IsCompare(loginViewModel.Password, loginViewModel.Password) == false)
        // {
        var result = await _userFacade.Login(new UserLoginCommand()
        {
            Password = loginViewModel.Password,
            UserName = loginViewModel.UserName,
            RememberMe = loginViewModel.rememberMe
        });
        
       // var loginResult = await AddTokenAndGenerateJwt(user);

        return CommandResult(result);
        //}

        //if (Sha256Hasher.IsCompare(user.Password, loginViewModel.Password) == false)
        //{
        //    var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
        //    return CommandResult(result);
        //}

        //var loginResult = await AddTokenAndGenerateJwt(user);
        //return CommandResult(loginResult);
    }

    [HttpPost("register")]
    public async Task<ApiResult> Register(RegisterViewModel register)
    {
        var command = new RegisterUserCommand(register.Email,register.UserName
            ,register.Password,register.PhoneNumber);
        var result = await _userFacade.RegisterUser(command);
        return CommandResult(result);
    }
    [HttpPost("RefreshToken")]
    public async Task<ApiResult<LoginResultDto?>> RefreshToken(string refreshToken)
    {
        var result = await _userFacade.GetUserTokenByRefreshToken(refreshToken);

        if (result == null)
            return CommandResult(OperationResult<LoginResultDto?>.NotFound());

        if (result.TokenExpireDate > DateTime.Now)
        {
            return CommandResult(OperationResult<LoginResultDto>.Error("توکن هنوز منقضی نشده است"));
        }

        if (result.RefreshTokenExpireDate < DateTime.Now)
        {
            return CommandResult(OperationResult<LoginResultDto>.Error("زمان رفرش توکن به پایان رسیده است"));
        }

        if (result.UserId == null)
        {
            return CommandResult(OperationResult<LoginResultDto>.NotFound());
        }
      
        var user = await _userFacade.GetUserById(result.UserId);
        if (user == null)
        {
            return CommandResult(OperationResult<LoginResultDto>.NotFound());
        }
        await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
        var loginResult = await AddTokenAndGenerateJwt(user);
        return CommandResult(loginResult);
    }

    [Authorize]
    [HttpDelete("logout")]
    public async Task<ApiResult> Logout()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        if (token == null)
        {
            return CommandResult(OperationResult.NotFound());
        }
       
        var result = await _userFacade.GetUserTokenByJwtToken(token);
        if (result == null)
            return CommandResult(OperationResult.NotFound());
        if (result.UserId == null)
        {
            return CommandResult(OperationResult.NotFound());
        }
        
        await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
        return CommandResult(OperationResult.Success());
    }
    [Authorize]
    [HttpPatch("confirmMobile")]
    public async Task<ApiResult> ConfirmMobile(ConfirmMobileCommand command)
    {
        var result = await _userFacade.MobileConfirmed(command);
        return CommandResult(result);
    }
    [Authorize]
    [HttpPatch("confirmEmail")]
    public async Task<ApiResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        var result = await _userFacade.EmailConfirmed(command);
        return CommandResult(result);
    }

    private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
    {
        var uaParser = Parser.GetDefault();
        var header = HttpContext.Request.Headers["user-agent"].ToString();
       // var header = HttpContext.Request.Headers["mailchimp_landing_site"].ToString();
        var device = "windows";
        if (header != null)
        {
            var info = uaParser.Parse(header);
            device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
        }

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        var refreshToken = Guid.NewGuid().ToString();

        var hashJwt = Sha256Hasher.Hash(token);
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);

        var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwt, hashRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));
        if (tokenResult.Status != OperationResultStatus.Success)
            return OperationResult<LoginResultDto?>.Error();

        return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
        {
            Token = token,
            RefreshToken = refreshToken
        });
    }
    [HttpPost("SendEmail")]

    public async Task<ApiResult<string>> send(SendEmailCommand command)
    {
        var result = await _userFacade.SendEmail(command);
        return CommandResult(result);
    }
    [HttpPost("GenerateTokenMobile")]

    public async Task<ApiResult<string>> GenerateMobileToken(GenerateTokenMobileConfirmedCommand command)
    {
        var result = await _userFacade.GenerateTokenMobileConfirmed(command);
        return CommandResult(result);
    }
    [HttpPost("GenerateTokenEmail")]

    public async Task<ApiResult<string>> GenerateEmailToken(Application.User.GenerateTokenEmailConfirmed.GenerateUserTokenConfirmEmailCommand command)
    {
        var result = await _userFacade.GenerateTokenEmailConfirmed(command);
        return CommandResult(result);
    }
}