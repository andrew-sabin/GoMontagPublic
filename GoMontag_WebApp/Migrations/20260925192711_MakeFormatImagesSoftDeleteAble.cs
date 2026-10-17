using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMontag_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class MakeFormatImagesSoftDeleteAble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "FormatImages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "FormatImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "FormatImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_FormatImages_ImageUrl",
                table: "FormatImages",
                column: "ImageUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FormatImages_ImageUrl",
                table: "FormatImages");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "FormatImages");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "FormatImages");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
