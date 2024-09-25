

namespace Blog.Domain.CategoryAgg.Services
{
    public interface ICategoryDomainService
    {
        bool IsTitleExist(string title);
        bool IsSlugExist(string slug);
    }
}
