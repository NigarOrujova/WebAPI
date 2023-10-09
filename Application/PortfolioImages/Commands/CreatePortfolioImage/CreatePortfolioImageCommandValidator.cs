using FluentValidation;

namespace Application.PortfolioImages.Commands.CreatePortfolioImage;

public class CreatePortfolioImageCommandValidator : AbstractValidator<CreatePortfolioImageCommand>
{
    public CreatePortfolioImageCommandValidator()
    {
        RuleFor(v => v.ImageAlt)
            .MaximumLength(200)
            .NotEmpty();
    }
}