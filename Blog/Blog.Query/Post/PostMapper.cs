using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Post.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Post
{
    public static class PostMapper
    {
        public static PostDto Map(this Domain.PostAgg.Post post, BlogContext context)
        {
            var userFullName = context.Users.Where(i => i.Id == post.UserId).Select(i => i.Name + " " + i.Family).FirstOrDefault();

            return new PostDto()
            {
                CategoryId = post.CategoryId,
                CreationDate = post.CreationDate,
                ImageName = post.ImageName,
                Description = post.Description,
                Id = post.Id,
                UserId = post.UserId,
                Slug = post.Slug,
                Title = post.Title,
                Visit = post.Visit,
                UserFullName = userFullName,
                MetaDescription = post.MetaDescription
            };
        }
        public static PostFilterDataDto MapListData(this Domain.PostAgg.Post post, BlogContext context)
        {
            var userFullName = context.Users.Where(i => i.Id == post.UserId).Select(i=>i.Name+" "+i.Family).FirstOrDefault();
            var title = context.Categories.Where(s => s.Id == post.CategoryId).Select(s => s.Title).FirstOrDefault();
            return new PostFilterDataDto()
            {
                Id = post.Id,
                CreationDate = post.CreationDate,
                ImageName = post.ImageName,
                Slug = post.Slug,
                Title = post.Title,
                Description = post.Description,
                Visit = post.Visit,
                CategoryTitle = title,
                UserFullName = userFullName,
                UserId = post.UserId,
                MetaDescription = post.MetaDescription
            };
        }
    }
}
