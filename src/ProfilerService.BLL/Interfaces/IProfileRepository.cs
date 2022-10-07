using ProfileService.BLL.Entities;

namespace ProfileService.BLL.Interfaces;

public interface IProfileRepository
{
    Task Create(Profile profile, CancellationToken token);

    Task Delete(Profile profile, CancellationToken token);

    Task Update(Profile profile, CancellationToken token);
}
