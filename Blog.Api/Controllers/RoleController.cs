using Blog.Application.Role.Create;
using Blog.Application.Role.Delete;
using Blog.Application.Role.Edit;
using Blog.Presentation.Facade.Role;
using Blog.Query.Role.DTOs;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiController
    {
        private readonly IRoleFacade _roleFacade;

        public RoleController(IRoleFacade roleFacade)
        {
            _roleFacade = roleFacade;
        }

        [HttpGet]
        public async Task<ApiResult<List<RoleDto>?>> GetRoles()
        {
            var result = await _roleFacade.GetRoles();
            return QueryResult(result);
        }

        [HttpGet("{roleId}")]
        public async Task<ApiResult<RoleDto?>> GetRoleById(string roleId)
        {
            var result = await _roleFacade.GetRoleById(roleId);
            return QueryResult(result);
        }

        [HttpGet("filter")]
        public async Task<ApiResult<RoleFilterResult?>> GetRoleByFilter([FromQuery]RoleFilterParam param)
        {
            var result = await _roleFacade.GetRolesByFilter(param);
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ApiResult> CreateRole(CreateRoleCommand command)
        {
            var result = await _roleFacade.CreateRole(command);
            return CommandResult(result);
        }

        [HttpPut]
        public async Task<ApiResult> EditRole(EditRoleCommand command)
        {
            var result = await _roleFacade.EditRole(command);
            return CommandResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteRole(string id)
        {
            var result = await _roleFacade.DeleteRole(new DeleteRoleCommand(id));
            return CommandResult(result);
        }
        
    }
}
