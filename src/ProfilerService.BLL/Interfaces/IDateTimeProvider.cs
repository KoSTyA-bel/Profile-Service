namespace ProfilerService.BLL.Interfaces;

public interface IDateTimeProvider
{
    DateTime NowUTC { get; }
}
