using FluentValidation;

namespace Application.PortfolioImages.Commands.UpdatePortfolioImage;

public class UpdatePortfolioImageCommandValidator : AbstractValidator<UpdatePortfolioImageCommand>
{
    public UpdatePortfolioImageCommandValidator()
    {
        RuleFor(v => v.ImageAlt)
            .MaximumLength(200);
    }
}