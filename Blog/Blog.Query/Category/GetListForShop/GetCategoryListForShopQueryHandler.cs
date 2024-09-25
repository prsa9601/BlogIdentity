using Blog.Query.Category.DTOs;
using Blog.Query.Category.GetList;
using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Category.GetListForShop
{
    public class GetCategoryListForShopQueryHandler : IQueryHandler<GetCategoryListForShopQuery, List<CategoryForShopDto>?>
    {
        private readonly BlogContext _context;

        public GetCategoryListForShopQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryForShopDto>?> Handle(GetCategoryListForShopQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Categories.OrderByDescending(i => i.Id).ToListAsync();
            return model.MapForShop(_context);
        }
    }
}
