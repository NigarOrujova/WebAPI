using FluentValidation;

namespace Application.OurValues.Commands.CreateOurValue;

public class CreateOurValueCommandValidator : AbstractValidator<CreateOurValueCommand>
{
    public CreateOurValueCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Description)
            .MaximumLength(1000)
            .NotEmpty();
    }
}