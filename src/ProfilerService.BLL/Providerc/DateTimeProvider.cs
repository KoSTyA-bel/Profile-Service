using ProfilerService.BLL.Interfaces;

namespace ProfilerService.BLL.Providerc;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime NowUTC => DateTime.UtcNow;
}
