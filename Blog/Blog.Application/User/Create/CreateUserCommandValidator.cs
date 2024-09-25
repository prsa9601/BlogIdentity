using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Blog.Application.User.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("UserName"));

            RuleFor(r => r.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("Name"));

            RuleFor(r => r.Family).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("Family"));

            //RuleFor(f => f.Avatar)
            //    .JustImageFile();

            RuleFor(r => r.PhoneNumber)
                .ValidPhoneNumber();
        }
    }
}
