using Blog.Application.User.AddToken;
using Blog.Application.User.ChangePassword;
using Blog.Application.User.ConfirmEmail;
using Blog.Application.User.ConfirmMobile;
using Blog.Application.User.Create;
using Blog.Application.User.Edit;
using Blog.Application.User.Register;
using Blog.Application.User.RemoveToken;
using Blog.Application.User.SetRole;
using Blog.Query.User.DTOs;
using Blog.Query.User.GetByFilter;
using Blog.Query.User.GetById;
using Blog.Query.User.GetByPhoneNumber;
using Blog.Query.User.UserTokens.GetByJwtToken;
using Blog.Query.User.UserTokens.GetByRefreshToken;
using Common.Application.SecurityUtil;
using Common.Application;
using Common.ChachHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Blog.Application.User.Delete;
using Blog.Application.User.Login;
using Blog.Application.User.SendEmail;
using Blog.Application.User.GenerateTokenMobileConfirmed;
using Blog.Application.User.GenerateTokenEmailConfirmed;

namespace Blog.Presentation.Facade.User
{
    public class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;
        //private IDistributedCache _cache;, IDistributedCache cache
        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
            //_cache = cache;
        }


        public async Task<OperationResult<string>> SendEmail(SendEmailCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<string>> GenerateTokenEmailConfirmed(GenerateUserTokenConfirmEmailCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<string>> GenerateTokenMobileConfirmed(GenerateTokenMobileConfirmedCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CreateUser(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> AddToken(AddUserTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Status != OperationResultStatus.Success)
                return OperationResult.Error();

            //await _cache.RemoveAsync(CacheKeys.UserToken(result.Data));
            return OperationResult.Success();
        }

        public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
        {
            //await _cache.RemoveAsync(CacheKeys.User(command.UserId));
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Login(UserLoginCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetRole(SetRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async  Task<OperationResult> EmailConfirmed(ConfirmEmailCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditUser(EditUserCommand command)
        {
            var result = await _mediator.Send(command);
            //if (result.Status == OperationResultStatus.Success)
                //await _cache.RemoveAsync(CacheKeys.User(command.UserId));
            return result;
        }

        public async Task<UserDto?> GetUserById(string userId)
        {
            //return await _cache.GetOrSet(CacheKeys.User(userId), () =>
            //{
                return await _mediator.Send(new GetUserByIdQuery(userId));
            //});
        }

        public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
        {
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
        }

        public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
        {
            var hashJwtToken = Sha256Hasher.Hash(jwtToken);
            //return await _cache.GetOrSet(CacheKeys.UserToken(hashJwtToken), () =>
            //{
                return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
            //});
        }

        public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
        {
            return await _mediator.Send(new GetUserByFilterQuery(filterParams));
        }

        public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
        }

        public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> MobileConfirmed(ConfirmMobileCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Delete(DeleteUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

