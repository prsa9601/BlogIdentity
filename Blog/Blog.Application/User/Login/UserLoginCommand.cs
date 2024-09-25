using Common.Application;
using Microsoft.AspNetCore.Identity;
using Common.Application.SecurityUtil;
using Blog.Domain.UserAgg.Services;

namespace Blog.Application.User.Login
{
    public class UserLoginCommand : IBaseCommand
    {
       // public string Name { get; set; }
        public string? UserName { get; set; }
       // public string PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
        //public string Email { get; set; }
    }
    public class UserLoginCommandHandler : IBaseCommandHandler<UserLoginCommand>
    {
        private readonly IUserDomainService _userService;
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly SignInManager<Domain.UserAgg.User> _signInManager;

        public UserLoginCommandHandler(UserManager<Domain.UserAgg.User> userManager, 
            SignInManager<Domain.UserAgg.User> signInManager, IUserDomainService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<OperationResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var user = _userManager.Users.FirstOrDefault(s => s.UserName == request.UserName);
                if(user == null) 
                    return OperationResult.NotFound();
                if (!Sha256Hasher.IsCompare(user.Password, request.Password))
                {
                    return OperationResult.Error("پسورد شما اشتباه است!");
                }
                
                await _signInManager.SignInAsync(user, request.RememberMe);
               
                // var result = await _signInManager.PasswordSignInAsync(user.Email!, user.Password, request.RememberMe, false);

                //await _userManager.CreateSecurityTokenAsync(user);
                

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
