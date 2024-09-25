using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Validation;
using FluentValidation;

namespace Blog.Application.Comment.Create
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(r => r.Text)
                .NotNull()
                .MinimumLength(2).WithMessage(ValidationMessages.minLength("متن نظر", 2));
        }
    }
}
