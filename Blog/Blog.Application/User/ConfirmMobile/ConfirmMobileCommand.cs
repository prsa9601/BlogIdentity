using System.Text;
using Blog.Domain.UserAgg.Repository;
using Common.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.User.ConfirmMobile
{
    public class ConfirmMobileCommand : IBaseCommand
    {
        public string PhoneNumber { get; set; }
        public string token { get; set; }
    }
    public class UserConfirmMobileCommandHandler : IBaseCommandHandler<ConfirmMobileCommand>
    {
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public UserConfirmMobileCommandHandler(UserManager<Domain.UserAgg.User> userManager, IUserRepository<Domain.UserAgg.User> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ConfirmMobileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
            if (user == null)
            {
              
            }

            bool result = await _userManager.VerifyTwoFactorTokenAsync(user, "Phone", request.token);
            //if (!result)
            //{
            //    return OperationResult.NotFound();
            //}

            user.PhoneNumberConfirmed = true;
            user.TwoFactorEnabled = true;
            await _userManager.UpdateAsync(user);
            return OperationResult.Success();
        }
    }
}
