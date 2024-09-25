using Blog.Application.Category.Create;
using Blog.Application.Category.Delete;
using Blog.Application.Category.Edit;
using Blog.Query.Category.DTOs;
using Common.Application;

namespace Blog.Presentation.Facade.Category
{
    public interface ICategoryFacade
    {

        Task<OperationResult> CreateCategory(CreateCategoryCommand command);
        Task<OperationResult> EditCategory(EditCategoryCommand command);
        Task<OperationResult> DeleteCategory(DeleteCategoryCommand command);


        Task<CategoryDto?> GetCommentById(long id);
        Task<List<CategoryDto>?> GetCategories();
        Task<List<CategoryForShopDto>?> GetCategoriesForShop();
        Task<CategoryForShopDto?> GetCategoryForShop(long id);
    }
}
