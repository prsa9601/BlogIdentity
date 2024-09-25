using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.CategoryAgg.Repository;
using Blog.Domain.CategoryAgg.Services;
using Common.Application;

namespace Blog.Application.Category.Create
{
    public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
    {
        private readonly ICategoryDomainService _service;
        private readonly ICategoryRepository _repository;

        public CreateCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.CategoryAgg.Category(request.Title, request.MetaTag, request.MetaDescription, request.Slug, _service);
            _repository.Add(category);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
