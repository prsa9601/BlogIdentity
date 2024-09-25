using Blog.Domain.CategoryAgg.Repository;
using Blog.Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistent.Ef.Category
{
    public class CategoryRepository : BaseRepository<Domain.CategoryAgg.Category>, ICategoryRepository
    {
        public CategoryRepository(BlogContext context) : base(context)
        {
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            var category = await Context.Categories.FirstOrDefaultAsync(i => i.Id == categoryId);
            if (category == null)
                return false;
            Context.Categories.Remove(category);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
