using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Blog.Application.Post.AddImage;

public class AddImageCommandHandlerValidator : AbstractValidator<AddImageCommand>
{
    public AddImageCommandHandlerValidator()
    {
        RuleFor(f => f.ImageFile)
            .JustImageFile();
    }

}