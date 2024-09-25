using Blog.Application._Utilities;
using Blog.Domain.PostAgg.Repository;
using Common.Application;
using Common.Application.FileUtil.Interfaces;

namespace Blog.Application.Post.AddImage;

public class AddImageCommandHandler : IBaseCommandHandler<AddImageCommand>
{
    private readonly IPostRepository _repository;
    private readonly IFileService _fileService;

    public AddImageCommandHandler(IPostRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetTracking(request.PostId);
        if (post == null)
            return OperationResult.NotFound();

        var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);

        post.AddImage(imageName);
        await _repository.Save();
        return OperationResult.Success();
    }
}