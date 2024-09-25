using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Blog.Application.Post.Edit;

public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
{
    public EditPostCommandValidator()
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

        RuleFor(f => f.ImageFile)
            .JustImageFile();

    }
}