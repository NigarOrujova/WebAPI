using FluentValidation;

namespace Application.OurValues.Commands.UpdateOurValue;

public class UpdateOurValueCommandValidator : AbstractValidator<UpdateOurValueCommand>
{
    public UpdateOurValueCommandValidator()
    {
        RuleFor(v => v.OurValue.Title)
            .MaximumLength(200);

        RuleFor(v => v.OurValue.Description)
            .MaximumLength(1000);
    }
}