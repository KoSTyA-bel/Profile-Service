namespace ProfileService.BLL.Interfaces;

public interface IDataContext
{
    Task SaveChanges(CancellationToken token);
}
