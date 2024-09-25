using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Blog.Application.Post.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(r => r.Slug)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Slug"));
            
            RuleFor(r => r.Title)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Title"));

             RuleFor(r => r.Description)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Description"));

             RuleFor(r => r.CategoryId)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("Category"));

             RuleFor(r => r.UserId)
                .NotNull().NotEmpty()
                .WithMessage(ValidationMessages.required("User"));

             RuleFor(f => f.ImageFile)
                 .JustImageFile();
        }
    }
}
