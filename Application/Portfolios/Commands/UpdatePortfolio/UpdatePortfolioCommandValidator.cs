using FluentValidation;

namespace Application.Portfolios.Commands.UpdatePortfolio;

public class UpdatePortfolioCommandValidator : AbstractValidator<UpdatePortfolioCommand>
{
    public UpdatePortfolioCommandValidator()
    {
        RuleFor(v => v.Portfolio.Title)
            .MaximumLength(200);

        RuleFor(v => v.Portfolio.Description)
            .MaximumLength(10000);
    }
}