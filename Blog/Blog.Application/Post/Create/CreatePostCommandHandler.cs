using Blog.Application._Utilities;
using Blog.Domain.PostAgg.Repository;
using Blog.Domain.PostAgg.Services;
using Common.Application;
using Common.Application.FileUtil.Interfaces;

namespace Blog.Application.Post.Create
{
    public class CreatePostCommandHandler : IBaseCommandHandler<CreatePostCommand>
    {
        private readonly IPostRepository _repository;
        private readonly IPostDomainService _service;
        private readonly IFileService _fileService;

        public CreatePostCommandHandler(IPostRepository repository, IPostDomainService service, IFileService fileService)
        {
            _repository = repository;
            _service = service;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile != null)
            {
                var imageName = await _fileService
                    .SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
                var post = new Domain.PostAgg.Post(request.UserId, imageName, request.MetaDescription, request.CategoryId, request.Title, request.Slug, request.Description, _service);
                _repository.Add(post);
                await _repository.Save();
                return OperationResult.Success();
            }
            return OperationResult.Error();
         
        }
    }
}
