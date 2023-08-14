using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_resolve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FR",
                table: "NotificationCategories");

            migrationBuilder.DropColumn(
                name: "SP",
                table: "NotificationCategories");

            migrationBuilder.RenameColumn(
                name: "WC",
                table: "NotificationCategories",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "NotificationCategories",
                newName: "WC");

            migrationBuilder.AddColumn<string>(
                name: "FR",
                table: "NotificationCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SP",
                table: "NotificationCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
