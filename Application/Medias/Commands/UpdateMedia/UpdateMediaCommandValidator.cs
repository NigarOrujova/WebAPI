using FluentValidation;

namespace Application.Medias.Commands.UpdateMedia;

public class UpdateMediaCommandValidator : AbstractValidator<UpdateMediaCommand>
{
    public UpdateMediaCommandValidator()
    {
        RuleFor(v => v.request.Title)
            .MaximumLength(200);

        RuleFor(v => v.request.URL)
            .MaximumLength(100);
    }
}