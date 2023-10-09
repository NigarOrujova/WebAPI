using FluentValidation;

namespace Application.Medias.Commands.UpdateMedia;

public class UpdateMediaCommandValidator : AbstractValidator<UpdateMediaCommand>
{
    public UpdateMediaCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200);

        RuleFor(v => v.URL)
            .MaximumLength(100);
    }
}