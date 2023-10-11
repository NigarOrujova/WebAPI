using FluentValidation;

namespace Application.Footers.Commands.UpdateFooter;

public class UpdateFooterCommandValidator : AbstractValidator<UpdateFooterCommand>
{
    public UpdateFooterCommandValidator()
    {
        RuleFor(v => v.Phone)
            .MaximumLength(20);

        RuleFor(v => v.Email)
            .MaximumLength(50);

        RuleFor(v => v.Address)
            .MaximumLength(100);
    }
}