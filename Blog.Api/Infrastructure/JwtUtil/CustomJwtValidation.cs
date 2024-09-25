using Blog.Presentation.Facade.User;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Api.Infrastructure.JwtUtil;

public class CustomJwtValidation
{
    private readonly IUserFacade _userFacade;

    public CustomJwtValidation(IUserFacade facade)
    {
        _userFacade = facade;
    }
    
    public async Task Validate(TokenValidatedContext context)
    {
        var userPhoneNumber = context.Principal!.GetPhoneNumber();
        var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var token = await _userFacade.GetUserTokenByJwtToken(jwtToken);
        if (token == null)
        {
            context.Fail("Token NotFound");
            return;
        }

        var user = await _userFacade.GetUserByPhoneNumber(userPhoneNumber);

    }
}