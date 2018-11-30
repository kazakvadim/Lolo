using Microsoft.EntityFrameworkCore.Migrations;

namespace Lolo.Migrations
{
    public partial class UpdatedLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_UserForeignKey",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UserForeignKey",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UserForeignKey",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Blogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "UserForeignKey",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserForeignKey",
                table: "Blogs",
                column: "UserForeignKey",
                unique: true,
                filter: "[UserForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_UserForeignKey",
                table: "Blogs",
                column: "UserForeignKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
