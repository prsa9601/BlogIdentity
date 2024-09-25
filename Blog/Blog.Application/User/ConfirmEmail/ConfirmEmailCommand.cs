using Blog.Domain.UserAgg;
using Common.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Blog.Application.User.ConfirmEmail
{
    public class ConfirmEmailCommand : IBaseCommand
    {
        public string UserId { get; set; }
        public string token { get; set; }

    }
    public class UserConfirmEmailCommandHandler : IBaseCommandHandler<ConfirmEmailCommand>
    {
        private readonly UserManager<Domain.UserAgg.User> _userManager;

        public UserConfirmEmailCommandHandler(UserManager<Domain.UserAgg.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return OperationResult.NotFound();

            string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return OperationResult.Success();
        }
    }
}
