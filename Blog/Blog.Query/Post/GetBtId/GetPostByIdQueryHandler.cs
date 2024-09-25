using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Post.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Post.GetBtId
{
    public class GetPostByIdQueryHandler(BlogContext context) : IQueryHandler<GetPostByIdQuery, PostDto?>
    {
        public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await context.Posts.FirstOrDefaultAsync(s => s.Id == request.PostId);
           
            //remind
            if (post == null) 
                return null!;
            
            //return new PostDto()
            //{
            //    CategoryId = post.CategoryId,
            //    CreationDate = post.CreationDate,
            //    Description = post.Description,
            //    Id = post.Id,
            //    UserId = post.UserId,
            //    Slug = post.Slug,
            //    Title = post.Title,
            //    Visit = post.Visit,
            //    ImageName = post.ImageName,
            //    UserFullName = userFullName
            //};
            return post.Map(context);
        }
    }
}
