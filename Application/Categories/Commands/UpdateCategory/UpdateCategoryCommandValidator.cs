using FluentValidation;

namespace Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Category.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}