using Common.Application;

namespace Blog.Application.Role.Delete
{
    public record class DeleteRoleCommand(string RoleId):IBaseCommand;
}
