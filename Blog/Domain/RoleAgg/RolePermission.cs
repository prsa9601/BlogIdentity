using Common.Domain;
using Blog.Domain.RoleAgg.Enums;

namespace Blog.Domain.RoleAgg
{
    public class RolePermission : BaseEntity
    {
        public RolePermission(Permission permission)
        {
            Permission = permission;
        }

        public string RoleId { get; internal set; }
        public Permission Permission { get; private set; }
    }
}
