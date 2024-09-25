using Blog.Domain.UserAgg.Repository;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        private readonly UserManager<Domain.UserAgg.User> _userManager;

        public ChangeUserPasswordCommandHandler(IUserRepository<Domain.UserAgg.User> userRepository, UserManager<Domain.UserAgg.User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<OperationResult> Handle(ChangeUserPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetTrackingWithString(request.UserId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد");

            var currentPasswordHash = Sha256Hasher.Hash(request.CurrentPassword);
            if (user.Password != currentPasswordHash)
            {
                return OperationResult.Error("کلمه عبور فعلی نامعتبر است");
            }

            var newPasswordHash = Sha256Hasher.Hash(request.Password);
            var result = await _userManager.RemovePasswordAsync(user);
            var result1 = await _userManager.AddPasswordAsync(user, newPasswordHash);
            user.ChangePassword(newPasswordHash);

            await _userRepository.Save();

            return OperationResult.Success();
        }
    }
}
