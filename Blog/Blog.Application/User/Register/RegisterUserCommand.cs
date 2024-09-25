using Common.Application.SecurityUtil;
using Common.Application;
using Microsoft.AspNetCore.Identity;
using Blog.Domain.UserAgg.Services;
using Blog.Domain.UserAgg.Repository;

namespace Blog.Application.User.Register
{
    public class RegisterUserCommand : IBaseCommand
    {
        public RegisterUserCommand(string email, string userName, string password, string phoneNumber)
        {
            Email = email;
            UserName = userName;
            Password = password;
            PhoneNumber = phoneNumber;
        }
        // public string Name { get; set; }
        public string Email { get; set; } 
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public string PhoneNumber { get; set; } 
        // public bool RememberMe { get; set; }
    }
    public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
    {
        private readonly IUserDomainService _userService;
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly UserManager<Blog.Domain.UserAgg.User> _userManager;
        private readonly SignInManager<Blog.Domain.UserAgg.User> _signInManager;

        public RegisterUserCommandHandler(
            UserManager<Domain.UserAgg.User> userManager, 
            SignInManager<Domain.UserAgg.User> signInManager,
            IUserDomainService userService, Domain.UserAgg.Repository
                .IUserRepository<Domain.UserAgg.User> repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool exist = _userManager.Users.Any(s => s.PhoneNumber == request.PhoneNumber);
                if (exist)
                    return OperationResult.Error("شما با این شماره تماس یک بار ثبت نام کردید!");

                string password = Sha256Hasher.Hash(request.Password);
                var result = await _userManager.CreateAsync(new Domain.UserAgg.User(request.Email, request.UserName,request.PhoneNumber, password, _userService) ,request.Password); 
                // var user = _userManager.Users.FirstOrDefault(s => s.PhoneNumber == request.PhoneNumber);, request.UserName
               // user.PhoneNumberConfirmed = true;
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        var error = new Exception(err.Description);
                        //ViewBag.IsSent = false;
                        //return View();
                    }
                    return OperationResult.Error(result.Errors.First().Description);
                }

                await _repository.Save();
                return OperationResult.Success();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
