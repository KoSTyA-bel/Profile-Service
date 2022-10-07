using ProfileService.BLL.Interfaces;

namespace ProfileService.BLL.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime NowUTC => DateTime.UtcNow;
}
