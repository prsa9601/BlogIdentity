using AutoMapper;
using Blog.Api.Infrastructure.Security;
using Blog.Api.ViewModels.Users;
using Blog.Application.Post.Delete;
using Blog.Application.User.ChangePassword;
using Blog.Application.User.Create;
using Blog.Application.User.Delete;
using Blog.Application.User.Edit;
using Blog.Application.User.SendEmail;
using Blog.Application.User.SetRole;
using Blog.Domain.RoleAgg.Enums;
using Blog.Presentation.Facade.User;
using Blog.Query.User.DTOs;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper;
        public UserController(IUserFacade userFacade, IMapper mapper)
        {
            _userFacade = userFacade;
            _mapper = mapper;
        }
        [PermissionChecker(Permission.User_Management)]
        [HttpGet]
        public async Task<ApiResult<UserFilterResult>> GetUsers([FromQuery] UserFilterParams filterParams)
        {
            var result = await _userFacade.GetUserByFilter(filterParams);
            return QueryResult(result);
        }
        [HttpGet("Current")]
        public async Task<ApiResult<UserDto?>> GetCurrentUser()
        {
            var result = await _userFacade.GetUserById(User.GetUserIdToString());
            return QueryResult(result);
        }

        [PermissionChecker(Permission.User_Management)]
        [HttpGet("{userId}")]
        public async Task<ApiResult<UserDto?>> GetById(string userId)
        {
            var result = await _userFacade.GetUserById(userId);
            return QueryResult(result);
        }

        [PermissionChecker(Permission.User_Management)]
        [HttpPost]
        public async Task<ApiResult> Create(CreateUserCommand command)
        {
            var result = await _userFacade.CreateUser(command);
            return CommandResult(result);
        }


        [HttpPut("ChangePassword")]
        public async Task<ApiResult> ChangePassword(ChangePasswordViewModel command)
        {
            var changePasswordModel = _mapper.Map<ChangeUserPasswordCommand>(command);
            changePasswordModel.UserId = User.GetUserIdToString();
            var result = await _userFacade.ChangePassword(changePasswordModel);
            return CommandResult(result);
        }

        [HttpPut("Current")]
        public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
        {
            var commandModel = new EditUserCommand(User.GetUserId(), command.Avatar, command.Name, command.Family,
                command.PhoneNumber, command.UserName);

            var result = await _userFacade.EditUser(commandModel);
            return CommandResult(result);
        }

       // [PermissionChecker(Permission.User_Management)]
        [HttpPut]
        public async Task<ApiResult> Edit([FromForm] EditUserModel command)
        {
            var result = await _userFacade.EditUser(new EditUserCommand(command.Id, command.Avatar, command.Name, command.Family,
                command.PhoneNumber, command.UserName));
            return CommandResult(result);
        }

        [HttpPatch("setRole")]
        public async Task<ApiResult> SetRole(SetRoleCommand command)
        {
            var result = await _userFacade.SetRole(command);
            return CommandResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            var result = await _userFacade.Delete(new DeleteUserCommand(id));
            return CommandResult(result);
        }
    }
}
