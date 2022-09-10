using Microsoft.EntityFrameworkCore;
using ProfilerService.BLL.Entities;
using ProfilerService.BLL.Interfaces;
using ProfilerService.DLL.Contexts;

namespace ProfilerService.DLL.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly DbSet<Profile> _profiles;

    public ProfileRepository(DbSet<Profile> profiles)
    {
        _profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
    }

    public Task Create(Profile profile, CancellationToken token)
    {
        _profiles.Add(profile);
        return Task.CompletedTask;
    }

    public Task Delete(Profile profile, CancellationToken token)
    {
        return Task.FromResult(_profiles.Remove(profile));
    }

    public Task Update(Profile profile, CancellationToken token)
    {
        return Task.FromResult(_profiles.Update(profile));
    }
}
