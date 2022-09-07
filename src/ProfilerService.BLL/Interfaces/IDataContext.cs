namespace ProfilerService.BLL.Interfaces;

public interface IDataContext
{
    Task SaveChanges(CancellationToken token);
}
