using AngleSharp.Dom;
using AngleSharp.Io;
using Common.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Blog.Domain.UserAgg.Repository;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;

namespace Blog.Application.User.GenerateTokenEmailConfirmed
{
    public class GenerateUserTokenConfirmEmailCommand : IBaseCommand<string>
    {
        public string Email { get; set; }
        //public string token { get; set; }
      
    }
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string token { get; set; }
    }
    public class UserConfirmEmailCommandHandler : IBaseCommandHandler<GenerateUserTokenConfirmEmailCommand, string>
    {
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public UserConfirmEmailCommandHandler(UserManager<Domain.UserAgg.User> userManager, IUserRepository<Domain.UserAgg.User> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<OperationResult<string>> Handle(GenerateUserTokenConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                const string exceptionNotFound = "کاربری با چنین مشخصاتی یافت نشد!";
                return OperationResult<string>.NotFound(exceptionNotFound);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
         //  string? callBackUrl = Url.ActionLink("ResetPassword", "Account", new { email = user.Email, token = token },
         //       Request.Scheme);
            //await _repository.SendEmail(user.Email, "بازیابی کلمه عبور", token);
            await _repository.Save();
            return OperationResult<string>.Success(token);
        }
    }
}
