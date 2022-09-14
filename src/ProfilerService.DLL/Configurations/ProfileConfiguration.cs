using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilerService.BLL.Entities;
using Npgsql.EntityFrameworkCore;

namespace ProfilerService.DLL.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.DiscrodId).IsRequired();
        builder.Property(x => x.Points).IsRequired();
        builder.Property(x => x.WaxWallet).IsRequired(false);
        builder.Property(x => x.LoseCount).IsRequired();
        builder.Property(x => x.WinCount).IsRequired();

        builder.ToTable("Profiles");
    }
}
