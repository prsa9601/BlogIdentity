using Blog.Application._Utilities;
using Blog.Domain.PostAgg.Repository;
using Blog.Domain.PostAgg.Services;
using Common.Application;
using Common.Application.FileUtil.Interfaces;

namespace Blog.Application.Post.Edit;

public class EditPostCommandHandler : IBaseCommandHandler<EditPostCommand>
{
    private readonly IPostRepository _repository;
    private readonly IPostDomainService _service;
    private readonly IFileService _fileService;

    public EditPostCommandHandler(IPostRepository repository, IPostDomainService service, IFileService fileService)
    {
        _repository = repository;
        _service = service;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetTracking(request.Id);
        if (post == null) 
            return OperationResult.NotFound();

        if (request.ImageFile != null)
        {
            var imageName = await _fileService
                .SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
            
            post.Edit(request.Title, imageName, request.MetaDescription, request.Description, request.Slug, request.CategoryId, _service);

        }

        await _repository.Save();
        return OperationResult.Success();
    }
}