namespace ProfileService.BLL.Interfaces;

public interface IDateTimeProvider
{
    DateTime NowUTC { get; }
}
