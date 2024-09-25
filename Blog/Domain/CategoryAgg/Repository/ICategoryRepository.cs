using Common.Domain.Repository;

namespace Blog.Domain.CategoryAgg.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> DeleteCategory(long categoryId);
    }
}
