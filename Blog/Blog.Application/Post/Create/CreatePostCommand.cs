using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Post.Create
{
    public class CreatePostCommand : IBaseCommand
    {
        //public CreatePostCommand(long userId, IFormFile imageFile, long categoryId, string title, string slug, string description)
        //{
        //    UserId = userId;
        //    ImageFile = imageFile;
        //    CategoryId = categoryId;
        //    Title = title;
        //    Slug = slug;
        //    Description = description;
        //}

        public string UserId { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; } 
        public long CategoryId { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
    }
}
