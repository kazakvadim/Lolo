using Microsoft.EntityFrameworkCore.Migrations;

namespace Lolo.Migrations
{
    public partial class TagChanged3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Posts",
                table: "Tags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Posts",
                table: "Tags",
                nullable: true);
        }
    }
}
