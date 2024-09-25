using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application._Utilities;
using Blog.Domain.UserAgg.Repository;
using Blog.Domain.UserAgg.Services;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.User.Edit
{
    public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly IUserDomainService _domainService;
        private readonly IFileService _fileService;
        public EditUserCommandHandler(IUserRepository<Domain.UserAgg.User> repository, IUserDomainService domainService, IFileService fileService)
        {
            _repository = repository;
            _domainService = domainService;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            var oldAvatar = user.AvatarName;
            user.Edit(request.Name, request.Family, request.PhoneNumber, request.UserName, _domainService);
            if (request.Avatar != null)
            {
                var imageName = await _fileService
                    .SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
                user.SetAvatar(imageName);
            }

            DeleteOldAvatar(request.Avatar, oldAvatar);
            await _repository.Save();
            return OperationResult.Success();
        }

        private void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
        {
            if (avatarFile == null || oldImage == "avatar.png")
                return;

            _fileService.DeleteFile(Directories.UserAvatars, oldImage);
        }
    }
}
