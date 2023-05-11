using YelloAd.Application.Common.Interfaces;

namespace YelloAd.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
