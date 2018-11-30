using Microsoft.EntityFrameworkCore.Migrations;

namespace Lolo.Migrations
{
    public partial class UpdatedPostModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
