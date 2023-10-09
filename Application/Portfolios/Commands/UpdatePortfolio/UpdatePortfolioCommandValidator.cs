using FluentValidation;

namespace Application.Portfolios.Commands.UpdatePortfolio;

public class UpdatePortfolioCommandValidator : AbstractValidator<UpdatePortfolioCommand>
{
    public UpdatePortfolioCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200);

        RuleFor(v => v.Description)
            .MaximumLength(10000);
    }
}