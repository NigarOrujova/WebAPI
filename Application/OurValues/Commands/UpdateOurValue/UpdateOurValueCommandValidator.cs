using FluentValidation;

namespace Application.OurValues.Commands.UpdateOurValue;

public class UpdateOurValueCommandValidator : AbstractValidator<UpdateOurValueCommand>
{
    public UpdateOurValueCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200);

        RuleFor(v => v.Description)
            .MaximumLength(1000);
    }
}