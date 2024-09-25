using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Blog.Api.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "شماره تلفن را وارد کنید")]
    [MaxLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    [MinLength(6,ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "نام کاربری را وارد کنید")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "ایمیل را وارد کنید")]
    [EmailAddress(ErrorMessage = "آدرس ایمیل نامعتبر است.")]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "تکرار کلمه عبور را وارد کنید")]
    [MinLength(6, ErrorMessage = "تکرار کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
    [Compare(nameof(Password),ErrorMessage = "کلمه های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; } = string.Empty;
}