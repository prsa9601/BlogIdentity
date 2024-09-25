using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Category.DTOs;
using Common.Domain.Exceptions;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Category.GetList
{
    public class GetCategoryListQueryHandler : IQueryHandler<GetCategoryListQuery, List<CategoryDto>?>
    {
        private readonly BlogContext _context;

        public GetCategoryListQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>?> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Categories.OrderByDescending(i => i.Id).ToListAsync();
            return model.Map();
        }
    }
}
