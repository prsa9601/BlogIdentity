using Blog.Query.Category.DTOs;
using Common.Query;

namespace Blog.Query.Category.GetForShop
{
    public record GetCategoryForShopQuery(long id) : IQuery<CategoryForShopDto?>;
}
