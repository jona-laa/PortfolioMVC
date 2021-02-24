using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioMVC.Data.Migrations
{
    public partial class AboutUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "About");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "About",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "About");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "About",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
