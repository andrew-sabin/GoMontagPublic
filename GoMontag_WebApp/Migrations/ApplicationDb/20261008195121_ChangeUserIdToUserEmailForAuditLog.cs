using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMontag_WebApp.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class ChangeUserIdToUserEmailForAuditLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AuditLogs",
                newName: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "AuditLogs",
                newName: "UserId");
        }
    }
}
