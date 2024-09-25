using Common.Application.Validation;
using FluentValidation;

namespace Blog.Application.Comment.Edit
{
    public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(r => r.Text)
                .NotNull()
                .MinimumLength(2).WithMessage(ValidationMessages.minLength("متن نظر", 2));


        }
    }
}
