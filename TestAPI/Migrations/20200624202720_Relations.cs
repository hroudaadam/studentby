using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAPI.Migrations
{
    public partial class Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobOfferId",
                table: "Registration",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Registration_JobOfferId",
                table: "Registration",
                column: "JobOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_JobOffer_JobOfferId",
                table: "Registration",
                column: "JobOfferId",
                principalTable: "JobOffer",
                principalColumn: "JobOfferId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_JobOffer_JobOfferId",
                table: "Registration");

            migrationBuilder.DropIndex(
                name: "IX_Registration_JobOfferId",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "JobOfferId",
                table: "Registration");
        }
    }
}
