using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        modelBuilder.Entity<Profile>().HasIndex(x => x.DiscrodId).IsUnique();
        modelBuilder.Entity<Profile>().HasIndex(x => x.WaxWallet).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
