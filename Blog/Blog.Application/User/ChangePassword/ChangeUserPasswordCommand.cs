using Common.Application;

namespace Blog.Application.User.ChangePassword
{
    public class ChangeUserPasswordCommand : IBaseCommand
    {
        public string UserId { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
