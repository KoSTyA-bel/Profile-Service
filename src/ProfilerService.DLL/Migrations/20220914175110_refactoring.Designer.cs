// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProfileService.DLL.Contexts;

#nullable disable

namespace ProfilerService.DLL.Migrations
{
    [DbContext(typeof(ProfileContext))]
    [Migration("20220914175110_refactoring")]
    partial class refactoring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProfilerService.BLL.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("DiscrodId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<long>("LoseCount")
                        .HasColumnType("bigint");

                    b.Property<int>("PointsAmount")
                        .HasColumnType("integer");

                    b.Property<string>("WaxWallet")
                        .HasColumnType("text");

                    b.Property<long>("WinCount")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DiscrodId")
                        .IsUnique();

                    b.HasIndex("WaxWallet")
                        .IsUnique();

                    b.ToTable("Profiles", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
