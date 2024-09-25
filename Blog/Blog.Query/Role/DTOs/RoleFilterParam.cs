using Common.Query.Filter;

namespace Blog.Query.Role.DTOs
{
    public class RoleFilterParam : BaseFilterParam
    {
        public string? Name { get; set; }
    }
}
