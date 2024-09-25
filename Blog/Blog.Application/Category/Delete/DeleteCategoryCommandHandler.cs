using Blog.Application.Category.Delete;
using Blog.Domain.CategoryAgg.Repository;
using Common.Application;

public class DeleteCategoryCommandHandler : IBaseCommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _repository;
    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteCategory(request.categoryId);
        if (!result)
            return OperationResult.Error();
        return OperationResult.Success();
    }
}
