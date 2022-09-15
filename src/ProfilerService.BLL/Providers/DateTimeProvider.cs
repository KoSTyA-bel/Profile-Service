using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime NowUTC => DateTime.UtcNow;
}
