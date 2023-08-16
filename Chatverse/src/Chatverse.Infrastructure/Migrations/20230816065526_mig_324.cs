using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_324 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "NotificationCategories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "NotificationCategories");
        }
    }
}
