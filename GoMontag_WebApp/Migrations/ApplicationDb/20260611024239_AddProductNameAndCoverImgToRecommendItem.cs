using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMontag_WebApp.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddProductNameAndCoverImgToRecommendItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategorySlug",
                table: "RecommendationLists");

            migrationBuilder.AddColumn<string>(
                name: "ProductCoverImg",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCoverImg",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Recommendations");

            migrationBuilder.AddColumn<string>(
                name: "CategorySlug",
                table: "RecommendationLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
