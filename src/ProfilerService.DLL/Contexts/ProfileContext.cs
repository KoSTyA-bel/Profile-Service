using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProfileService.DLL.Configurations;
using ProfileService.BLL.Entities;

namespace ProfileService.DLL.Contexts;

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
