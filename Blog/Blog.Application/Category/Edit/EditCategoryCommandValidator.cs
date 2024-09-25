using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Validation;
using FluentValidation;

namespace Blog.Application.Category.Edit
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {

            RuleFor(r => r.Title)
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.Slug)
                .NotEmpty().WithMessage(ValidationMessages.required("Slug"));

            RuleFor(r => r.MetaTag)
                .NotEmpty().WithMessage(ValidationMessages.required("MetaTag"));

            RuleFor(r => r.MetaDescription)
                .NotEmpty().WithMessage(ValidationMessages.required("MetaDescription"));

        }
    }
}
