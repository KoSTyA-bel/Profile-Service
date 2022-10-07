using ProfileService.BLL.Interfaces;

namespace ProfileService.DLL.Contexts;

public class ProfileDataContext : IDataContext
{
    private readonly ProfileContext _profileContext;

    public ProfileDataContext(ProfileContext profileContext)
    {
        _profileContext = profileContext ?? throw new ArgumentNullException(nameof(profileContext));
    }

    public Task SaveChanges(CancellationToken token)
    {
        return _profileContext.SaveChangesAsync(token);
    }
}
