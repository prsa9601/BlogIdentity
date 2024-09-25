using System.ComponentModel.DataAnnotations;

namespace Blog.Api.ViewModels.Users;

public class ChangePasswordViewModel
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; } = string.Empty;
}