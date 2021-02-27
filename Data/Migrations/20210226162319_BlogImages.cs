using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioMVC.Data.Migrations
{
    public partial class BlogImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Posts",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Posts");
        }
    }
}
