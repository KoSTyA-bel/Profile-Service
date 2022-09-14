using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfilerService.DLL.Migrations
{
    public partial class refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Points",
                table: "Profiles",
                newName: "PointsAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PointsAmount",
                table: "Profiles",
                newName: "Points");
        }
    }
}
