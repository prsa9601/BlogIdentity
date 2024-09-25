using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.CategoryAgg.Repository;
using Blog.Domain.CategoryAgg.Services;
using Common.Application;

namespace Blog.Application.Category.Edit
{
    public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryDomainService _service;
        private readonly ICategoryRepository _repository;

        public EditCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetTracking(request.Id);
            if (category == null) 
                return OperationResult.NotFound();
            category.Edit(request.Title, request.Slug, request.MetaTag, request.MetaDescription, _service);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
