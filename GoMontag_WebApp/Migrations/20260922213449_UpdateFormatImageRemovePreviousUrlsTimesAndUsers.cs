using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMontag_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFormatImageRemovePreviousUrlsTimesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousTimes",
                table: "FormatImages");

            migrationBuilder.DropColumn(
                name: "PreviousUrls",
                table: "FormatImages");

            migrationBuilder.DropColumn(
                name: "PreviousUsers",
                table: "FormatImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousTimes",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviousUrls",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviousUsers",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
