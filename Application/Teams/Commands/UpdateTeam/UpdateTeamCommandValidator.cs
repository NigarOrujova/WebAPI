using FluentValidation;

namespace Application.Teams.Commands.UpdateTeam;

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamCommandValidator()
    {
        RuleFor(v => v.FulllName)
            .MaximumLength(200);

        RuleFor(v => v.Job)
            .MaximumLength(100);
    }
}