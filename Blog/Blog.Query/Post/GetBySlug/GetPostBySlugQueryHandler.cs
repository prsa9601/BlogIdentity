using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Post.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Post.GetBySlug
{
    public class GetPostBySlugQueryHandler : IQueryHandler<GetPostBySlugQuery, PostDto?>
    {
        private readonly BlogContext _context;

        public GetPostBySlugQueryHandler(BlogContext context)
        {
            _context = context;
        }

        public async Task<PostDto?> Handle(GetPostBySlugQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(s => s.Slug == request.slug);
            if (post == null)
                return null;

            return new PostDto()
            {
                CategoryId = post.CategoryId,
                CreationDate = post.CreationDate,
                Description = post.Description,
                Id = post.Id,
                UserId = post.UserId,
                Slug = post.Slug,
                Title = post.Title,
                Visit = post.Visit,
                ImageName = post.ImageName,
                MetaDescription = post.MetaDescription
                
            };

        }
    }
}
