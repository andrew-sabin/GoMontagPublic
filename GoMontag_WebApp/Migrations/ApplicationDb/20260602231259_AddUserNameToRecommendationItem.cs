using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMontag_WebApp.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddUserNameToRecommendationItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Recommendations");
        }
    }
}
