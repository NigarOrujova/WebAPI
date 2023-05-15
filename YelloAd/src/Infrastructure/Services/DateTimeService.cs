using Yelload.Application.Common.Interfaces;

namespace Yelload.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
