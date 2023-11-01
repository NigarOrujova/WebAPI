using FluentValidation;

namespace Application.PortfolioImages.Commands.UpdatePortfolioImage;

public class UpdatePortfolioImageCommandValidator : AbstractValidator<UpdatePortfolioImageCommand>
{
    public UpdatePortfolioImageCommandValidator()
    {
        RuleFor(v => v.PortfolioImage.ImageAlt)
            .MaximumLength(200);
    }
}