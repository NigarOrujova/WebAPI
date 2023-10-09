using FluentValidation;

namespace Application.Teams.Commands.CreateTeam;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(v => v.FulllName)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Job)
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(v => v.ImageAlt)
            .MaximumLength(200)
            .NotEmpty();
    }
}