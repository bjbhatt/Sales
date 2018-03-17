using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sales.Migrations
{
    public partial class AddedURLForImagesToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                schema: "dbo",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailURL",
                schema: "dbo",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailURL",
                schema: "dbo",
                table: "Products");
        }
    }
}
