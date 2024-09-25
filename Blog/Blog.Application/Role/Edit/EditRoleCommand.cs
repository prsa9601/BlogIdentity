using Blog.Domain.RoleAgg.Enums;
using Common.Application;

namespace Blog.Application.Role.Edit
{
    public record EditRoleCommand(string Id, string Name, List<Permission> Permissions) : IBaseCommand;

}
