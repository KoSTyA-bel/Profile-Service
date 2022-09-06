namespace ProfilerService.BLL.Interfaces;

public interface IDataContext
{
    public Task SaveChangesAsync(CancellationToken token);
}
