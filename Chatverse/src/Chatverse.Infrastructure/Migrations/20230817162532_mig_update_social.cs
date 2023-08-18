using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_social : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialAccount_AspNetUsers_UserId",
                table: "SocialAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialAccount",
                table: "SocialAccount");

            migrationBuilder.RenameTable(
                name: "SocialAccount",
                newName: "SocialAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_SocialAccount_UserId",
                table: "SocialAccounts",
                newName: "IX_SocialAccounts_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "SocialAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialAccounts",
                table: "SocialAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialAccounts_AspNetUsers_UserId",
                table: "SocialAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialAccounts_AspNetUsers_UserId",
                table: "SocialAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialAccounts",
                table: "SocialAccounts");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "SocialAccounts");

            migrationBuilder.RenameTable(
                name: "SocialAccounts",
                newName: "SocialAccount");

            migrationBuilder.RenameIndex(
                name: "IX_SocialAccounts_UserId",
                table: "SocialAccount",
                newName: "IX_SocialAccount_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialAccount",
                table: "SocialAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialAccount_AspNetUsers_UserId",
                table: "SocialAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
