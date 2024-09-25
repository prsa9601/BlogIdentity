using Common.Application.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Validation.FluentValidations;

namespace Blog.Application.User.Edit
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("UserName"));

            RuleFor(r => r.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("Name"));

            RuleFor(r => r.Family).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("Family"));

            RuleFor(f => f.Avatar)
                .JustImageFile();

            RuleFor(r => r.PhoneNumber)
                .ValidPhoneNumber();


        }
    }
}
