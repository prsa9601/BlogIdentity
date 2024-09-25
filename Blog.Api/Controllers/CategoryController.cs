using Blog.Application.Category.Create;
using Blog.Application.Category.Delete;
using Blog.Application.Category.Edit;
using Blog.Presentation.Facade.Category;
using Blog.Query.Category.DTOs;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiController
    {
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICategoryFacade _facade;
        public CategoryController(ICategoryFacade facade)
        {
            _facade = facade;
        }
        [HttpPost]
        public async Task<ApiResult> Create(CreateCategoryCommand command)
        {
            var result = await _facade.CreateCategory(command);
            return CommandResult(result);
        }
        [HttpPatch]
        public async Task<ApiResult> Edit(EditCategoryCommand command)
        {
            var result = await _facade.EditCategory(command);
            return CommandResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            var result = await _facade.DeleteCategory(new DeleteCategoryCommand(id));
            return CommandResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<CategoryDto?>> GetById(long id)
        {
            var result = await _facade.GetCommentById(id);
            return QueryResult(result);
        }
        [HttpGet]
        public async Task<ApiResult<List<CategoryDto>?>> GetCategories()
        {
            var result = await _facade.GetCategories();
            return QueryResult(result);
        }
        [HttpGet("CategoriesForShop")]
        public async Task<ApiResult<List<CategoryForShopDto>?>> GetCategoriesForShop()
        {
            var result = await _facade.GetCategoriesForShop();
            return QueryResult(result);
        }
        [HttpGet("CategoryForShop/{id}")]
        public async Task<ApiResult<CategoryForShopDto?>> GetCategoryForShop(long id)
        {
            var result = await _facade.GetCategoryForShop(id);
            return QueryResult(result);
        }
    }
}
