using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMontag_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFormatImageWithImageNameAndPreviousImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FormatImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "FormatImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PreviousTimes",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PreviousUrls",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PreviousUsers",
                table: "FormatImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FormatImages");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "FormatImages");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "FormatImages");

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
    }
}
