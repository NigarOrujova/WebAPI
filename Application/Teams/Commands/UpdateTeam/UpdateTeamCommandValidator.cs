using FluentValidation;

namespace Application.Teams.Commands.UpdateTeam;

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamCommandValidator()
    {
        RuleFor(v => v.Team.FulllName)
            .MaximumLength(200);

        RuleFor(v => v.Team.Job)
            .MaximumLength(100);
    }
}