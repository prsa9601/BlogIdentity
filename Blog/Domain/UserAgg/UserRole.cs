using Common.Domain;

namespace Blog.Domain.UserAgg;

public class UserRole : BaseEntity
{
    public UserRole(string roleName)
    {
        RoleName = roleName;
    }
    public string UserId { get; set; }
    //public string RoleId { get; set; }
    public string RoleName { get; set; }
    
}