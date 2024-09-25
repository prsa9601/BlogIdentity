using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Category.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Category.GetForShop
{
    public class GetCategoryForShopQueryHandler : IQueryHandler<GetCategoryForShopQuery,CategoryForShopDto?>
    {
        private readonly BlogContext _context;

        public GetCategoryForShopQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<CategoryForShopDto?> Handle(GetCategoryForShopQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Categories.OrderByDescending(i => i.Id).FirstOrDefaultAsync();
            //var Posts = _context.Posts.Where(i => i.CategoryId == request.id).ToList();
           
            return model.MapCategoryForShop(_context);
        }
    }
}
