using Blog.Query.Category.DTOs;
using Blog.Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Category
{
    public static class CategoryMapper
    {
        public static List<CategoryDto>? Map(this List<Domain.CategoryAgg.Category> categories)
        {
            var model = new List<CategoryDto>();

            categories.ForEach(category =>
            {
                model.Add(new CategoryDto()
                {
                    Title = category.Title,
                    Slug = category.Slug,
                    Id = category.Id,
                    MetaDescription = category.MetaDescription,
                    CreationDate = category.CreationDate,
                    MetaTag = category.MetaTag,
                });
            });

            return model;
        }
        public static List<CategoryForShopDto> MapForShop(this List<Domain.CategoryAgg.Category> categories, BlogContext context)
        {
            var model = new List<CategoryForShopDto>();

            categories.ForEach(category =>
            {
                model.Add(new CategoryForShopDto()
                {
                    Title = category.Title,
                    Slug = category.Slug,
                    Id = category.Id,
                    MetaDescription = category.MetaDescription,
                    CreationDate = category.CreationDate,
                    MetaTag = category.MetaTag,
                    Posts = context.Posts.Where(i=>i.CategoryId == category.Id).ToList().MapPost(context)
                });
            });

            return model;
        }
        public static List<PostCategoryDto> MapPost(this List<Domain.PostAgg.Post> posts, BlogContext context)
        {
            var model = new List<PostCategoryDto>();

            posts.ForEach(post =>
            {
                model.Add(new PostCategoryDto()
                {
                    Title = post.Title,
                    Slug = post.Slug,
                    Id = post.Id,
                    CreationDate = post.CreationDate,
                    UserId = post.UserId,
                    Visit = post.Visit,
                    Description = post.Description,
                    ImageName = post.ImageName,
                    CategoryId = post.CategoryId,
                    UserFullName = context.Users.Where(i=>i.Id == post.UserId).Select(i=>i.Name + i.Family).FirstOrDefault()
                });
            });

            return model;
        }
        public static CategoryForShopDto MapCategoryForShop(this Domain.CategoryAgg.Category category, BlogContext context)
        {


            return new CategoryForShopDto()
            {
                Title = category.Title,
                Slug = category.Slug,
                Id = category.Id,
                MetaDescription = category.MetaDescription,
                CreationDate = category.CreationDate,
                MetaTag = category.MetaTag,
                Posts = context.Posts.Where(i => i.Id == category.Id).ToList().MapPost(context)
            };

         
        }
    }
}
