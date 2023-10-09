using Domain.Entities.Membership;

namespace Application.Abstracts.Common.Interfaces;
public interface ITokenService
{
    string BuildToken(AppUser user);
    bool ValidateToken(string token);
}
