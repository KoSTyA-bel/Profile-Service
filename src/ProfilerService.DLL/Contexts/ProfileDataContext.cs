using ProfilerService.BLL.Interfaces;

namespace ProfilerService.DLL.Contexts;

public class ProfileDataContext : IDataContext
{
    private readonly ProfileContext _profileContext;

    public ProfileDataContext(ProfileContext profileContext)
    {
        _profileContext = profileContext ?? throw new ArgumentNullException(nameof(profileContext));
    }

    public Task SaveChangesAsync(CancellationToken token) => _profileContext.SaveChangesAsync(token);
}
