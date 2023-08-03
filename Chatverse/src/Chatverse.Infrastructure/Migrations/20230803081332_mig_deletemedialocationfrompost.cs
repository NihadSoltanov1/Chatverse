using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_deletemedialocationfrompost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaLocation",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaLocation",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
