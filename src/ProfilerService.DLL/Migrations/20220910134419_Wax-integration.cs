using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfilerService.DLL.Migrations
{
    public partial class Waxintegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscrodId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    WaxWallet = table.Column<string>(type: "text", nullable: true),
                    Points = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
