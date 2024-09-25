using Blog.Application.Role.Create;
using Blog.Application.Role.Edit;
using Blog.Query.Role.DTOs;
using Blog.Query.Role.GetById;
using Common.Application;
using Blog.Query.Role.GetByFilter;
using Blog.Query.Role.GetList;
using MediatR;
using Blog.Application.Role.Delete;

namespace Blog.Presentation.Facade.Role
{
    public class RoleFacade : IRoleFacade
    {
        private readonly IMediator _mediator;

        public RoleFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateRole(CreateRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditRole(EditRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<RoleDto?> GetRoleById(string roleId)
        {
            return await _mediator.Send(new GetRoleByIdQuery(roleId));
        }

        public async Task<RoleFilterResult?> GetRolesByFilter(RoleFilterParam param)
        {
            return await _mediator.Send(new GetRoleByFilterQuery(param));
        }
        public async Task<List<RoleDto>?> GetRoles()
        {
            return await _mediator.Send(new GetRoleListQuery());
        }

        public async Task<OperationResult> DeleteRole(DeleteRoleCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
