using AngleSharp.Dom;
using AngleSharp.Io;
using Common.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Blog.Domain.UserAgg.Repository;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;

namespace Blog.Application.User.GenerateTokenMobileConfirmed
{
    public class GenerateTokenMobileConfirmedCommand : IBaseCommand<string>
    {
        public string PhoneNumber { get; set; }
        //public string token { get; set; }
      
    }
   
    public class GenerateTokenMobileConfirmedCommandHandler : IBaseCommandHandler<GenerateTokenMobileConfirmedCommand, string>
    {
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public GenerateTokenMobileConfirmedCommandHandler(UserManager<Domain.UserAgg.User> userManager, IUserRepository<Domain.UserAgg.User> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<OperationResult<string>> Handle(GenerateTokenMobileConfirmedCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingByPhoneNumber(request.PhoneNumber);
            if (user == null)
            {
                const string exceptionNotFound = "کاربری با چنین مشخصاتی یافت نشد!";
                return OperationResult<string>.NotFound(exceptionNotFound);
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Phone");
           // token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
         //  string? callBackUrl = Url.ActionLink("ResetPassword", "Account", new { email = user.Email, token = token },
         //       Request.Scheme);
            //await _repository.SendEmail(user.Email, "بازیابی کلمه عبور", token);
            await _repository.Save();
            return OperationResult<string>.Success(token);
        }
    }
}
