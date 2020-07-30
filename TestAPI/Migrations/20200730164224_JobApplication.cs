using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAPI.Migrations
{
    public partial class JobApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    JobApplicationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: false),
                    JobOfferId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.JobApplicationId);
                    table.ForeignKey(
                        name: "FK_JobApplication_JobOffer_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffer",
                        principalColumn: "JobOfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplication_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobOfferId",
                table: "JobApplication",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_StudentId",
                table: "JobApplication",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobOfferId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registration_JobOffer_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffer",
                        principalColumn: "JobOfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registration_JobOfferId",
                table: "Registration",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_StudentId",
                table: "Registration",
                column: "StudentId");
        }
    }
}
