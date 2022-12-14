using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore;
using ProfileService.BLL.Entities;

namespace ProfileService.DLL.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.DiscrodId).IsRequired();
        builder.Property(x => x.PointsAmount).IsRequired();
        builder.Property(x => x.WaxWallet).IsRequired(false);
        builder.Property(x => x.LoseCount).IsRequired();
        builder.Property(x => x.WinCount).IsRequired();

        builder.ToTable("Profiles");
    }
}
