using Blog.Domain.RoleAgg.Enums;
using Common.Application;

namespace Blog.Application.Role.Create
{
    public record CreateRoleCommand(string Name, List<Permission> Permissions) : IBaseCommand;

}
