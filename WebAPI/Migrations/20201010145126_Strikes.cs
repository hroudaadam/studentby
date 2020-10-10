using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Strikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Strikes",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Group",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Strikes",
                table: "Group",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strikes",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Strikes",
                table: "Group");
        }
    }
}
