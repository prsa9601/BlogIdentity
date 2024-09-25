using Blog.Domain.RoleAgg.Enums;
using Common.Query;
using Microsoft.AspNetCore.Identity;

namespace Blog.Query.Role.DTOs
{
    public class RoleFilterData : IdentityRole
    {
        //public string? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Title { get; set; }
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
