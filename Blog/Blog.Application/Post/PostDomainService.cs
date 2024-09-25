using Blog.Domain.PostAgg.Repository;
using Blog.Domain.PostAgg.Services;

namespace Blog.Application.Post
{
    public class PostDomainService : IPostDomainService
    {
        private readonly IPostRepository _repository;

        public 
            PostDomainService(IPostRepository repository)
        {
            _repository = repository;
        }

        public bool IsSlugExist(string slug)
        {
            return _repository.Exists(s => s.Slug == slug);
        }
    }
}
