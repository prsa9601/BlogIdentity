using Blog.Application.Category.Create;
using Blog.Application.Category.Delete;
using Blog.Application.Category.Edit;
using Blog.Query.Category.DTOs;
using Blog.Query.Category.GetById;
using Blog.Query.Category.GetForShop;
using Blog.Query.Category.GetList;
using Blog.Query.Category.GetListForShop;
using Common.Application;
using MediatR;

namespace Blog.Presentation.Facade.Category
{
    public class CategoryFacade : ICategoryFacade
    {
        private readonly IMediator _mediator;

        public CategoryFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditCategory(EditCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CategoryDto?> GetCommentById(long id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(id));
        }

        //public async Task<List<CategoryDto?>> GetCategoryByFilter()
        //{
        //    return await _mediator.Send(new GetCategoryListQuery());
        //}

        public async Task<OperationResult> DeleteCategory(DeleteCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<CategoryDto>?> GetCategories()
        {
            return await _mediator.Send(new GetCategoryListQuery());
        }

        public async Task<List<CategoryForShopDto>?> GetCategoriesForShop()
        {
            return await _mediator.Send(new GetCategoryListForShopQuery());
        }

        public async Task<CategoryForShopDto?> GetCategoryForShop(long id)
        {
            return await _mediator.Send(new GetCategoryForShopQuery(id));
        }
    }
}
