using FluentValidation;

namespace Application.Medias.Commands.CreateMedia;

public class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
{
    public CreateMediaCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.URL)
            .MaximumLength(100)
            .NotEmpty();
    }
}