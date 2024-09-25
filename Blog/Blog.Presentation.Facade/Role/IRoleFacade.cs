using Blog.Application.Role.Create;
using Blog.Application.Role.Delete;
using Blog.Application.Role.Edit;
using Blog.Query.Role.DTOs;
using Blog.Query.Role.GetByFilter;
using Common.Application;

namespace Blog.Presentation.Facade.Role
{
    public interface IRoleFacade
    {
        Task<OperationResult> CreateRole(CreateRoleCommand command);
        Task<OperationResult> EditRole(EditRoleCommand command);
        Task<OperationResult> DeleteRole(DeleteRoleCommand command);

        Task<RoleDto?> GetRoleById(string roleId);
        Task<RoleFilterResult?> GetRolesByFilter(RoleFilterParam param);
        Task<List<RoleDto>?> GetRoles();
    }
}
