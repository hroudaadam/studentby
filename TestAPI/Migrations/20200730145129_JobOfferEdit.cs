using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAPI.Migrations
{
    public partial class JobOfferEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JobOffer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "JobOffer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Spaces",
                table: "JobOffer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "JobOffer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "JobOffer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Wage",
                table: "JobOffer",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "End",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "Spaces",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "Wage",
                table: "JobOffer");
        }
    }
}
