using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynCore.Api.Migrations
{
    /// <inheritdoc />
    public partial class Add_WasUsed_PasswordResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WasUsed",
                table: "PasswordResetTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasUsed",
                table: "PasswordResetTokens");
        }
    }
}
