using Microsoft.EntityFrameworkCore;
using ProfilerService.BLL.Entities;
using ProfilerService.DLL.Configurations;

namespace ProfilerService.DLL.Contexts;

public class ProfileContext : DbContext
{
    public ProfileContext(DbContextOptions<ProfileContext> options)
        : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
