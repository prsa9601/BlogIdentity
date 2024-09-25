using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Category.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Category.GetById;

public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly BlogContext _context;

    public GetCategoryByIdQueryHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == request.categoryId);
        if (category != null)
        {
            return new CategoryDto()
            {
                Title = category.Title,
                Slug = category.Slug,
                Id = category.Id,
                MetaDescription = category.MetaDescription,
                CreationDate = category.CreationDate,
                MetaTag = category.MetaTag
            };
        }
        return null;
    }
}