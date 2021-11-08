using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameAPI.Migrations
{
    public partial class AddDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Characters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Characters",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Characters");
        }
    }
}
