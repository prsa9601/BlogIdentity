using Blog.Query.Category.DTOs;
using Common.Query;

namespace Blog.Query.Category.GetList
{
    public record class GetCategoryListQuery:IQuery<List<CategoryDto>?>;

}
