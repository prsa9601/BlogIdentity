using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Blog.Api.ViewModels.Auth;

public class LoginViewModel
{
    //[Required(ErrorMessage = "شماره تلفن را وارد کنید")]
    //[MaxLength(11,ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    //[MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    //public string PhoneNumber { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public bool rememberMe { get; set; } 

    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    public string Password { get; set; } = string.Empty;
}