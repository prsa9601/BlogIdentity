using Blog.Domain.CategoryAgg.Repository;
using Blog.Domain.CategoryAgg.Services;

namespace Blog.Application.Category
{
    public class CategoryDomainService : ICategoryDomainService
    {
        private readonly ICategoryRepository _repository;

        public CategoryDomainService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public bool IsTitleExist(string title)
        {
            return _repository.Exists(s => s.Title == title);
        }

        public bool IsSlugExist(string slug)
        {
            return _repository.Exists(s => s.Slug == slug);
        }
    }
}
