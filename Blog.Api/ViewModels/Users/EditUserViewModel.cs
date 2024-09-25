using System.ComponentModel.DataAnnotations;
using Common.Application.Validation.CustomValidation.IFormFile;

namespace Blog.Api.ViewModels.Users;

public class EditUserViewModel
{
    [Display(Name = "عکس پروفایل")]
    [FileImage(ErrorMessage = "تصویر پروفایل نامعتبر است")]
    public IFormFile? Avatar { get; set; }

    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; } = string.Empty;

    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; } = string.Empty;


    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string UserName { get; set; } = string.Empty;

}
public class EditUserModel
{
    [Display(Name = "Id")]
    public long Id { get; set; }

    [Display(Name = "عکس پروفایل")]
    [FileImage(ErrorMessage = "تصویر پروفایل نامعتبر است")]
    public IFormFile? Avatar { get; set; }

    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; } = string.Empty;

    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string UserName { get; set; } = string.Empty;

}