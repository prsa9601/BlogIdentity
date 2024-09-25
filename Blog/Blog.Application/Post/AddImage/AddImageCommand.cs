using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Post.AddImage
{
    public class AddImageCommand : IBaseCommand
    {
        //remind
        public IFormFile ImageFile { get; set; } = null!;
        public long PostId { get; set; }
        //public int Sequence { get; set; }
    }
}
