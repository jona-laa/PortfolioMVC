﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioMVC.Data.Migrations
{
    public partial class ImageDescriptionBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageDescription",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageDescription",
                table: "Posts");
        }
    }
}
