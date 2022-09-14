using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfilerService.DLL.Migrations
{
    public partial class NewFunc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Profiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "LoseCount",
                table: "Profiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WinCount",
                table: "Profiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_DiscrodId",
                table: "Profiles",
                column: "DiscrodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_WaxWallet",
                table: "Profiles",
                column: "WaxWallet",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profiles_DiscrodId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_WaxWallet",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "LoseCount",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "WinCount",
                table: "Profiles");

            migrationBuilder.AlterColumn<double>(
                name: "Points",
                table: "Profiles",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
