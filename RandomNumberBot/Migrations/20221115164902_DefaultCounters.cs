using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomNumberBot.Migrations
{
    public partial class DefaultCounters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RegionCounter",
                table: "RegionCounter");

            migrationBuilder.RenameTable(
                name: "RegionCounter",
                newName: "RegionCounters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegionCounters",
                table: "RegionCounters",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RegionCounters",
                table: "RegionCounters");

            migrationBuilder.RenameTable(
                name: "RegionCounters",
                newName: "RegionCounter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegionCounter",
                table: "RegionCounter",
                column: "Id");
        }
    }
}
