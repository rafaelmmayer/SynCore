using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynCore.Api.Migrations
{
    /// <inheritdoc />
    public partial class Change_Time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "ClassTimes");

            migrationBuilder.DropColumn(
                name: "Minute",
                table: "ClassTimes");

            migrationBuilder.AddColumn<string>(
                name: "EndHour",
                table: "ClassTimes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndMinute",
                table: "ClassTimes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartHour",
                table: "ClassTimes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartMinute",
                table: "ClassTimes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "ClassTimes");

            migrationBuilder.DropColumn(
                name: "EndMinute",
                table: "ClassTimes");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "ClassTimes");

            migrationBuilder.DropColumn(
                name: "StartMinute",
                table: "ClassTimes");

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "ClassTimes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Minute",
                table: "ClassTimes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
