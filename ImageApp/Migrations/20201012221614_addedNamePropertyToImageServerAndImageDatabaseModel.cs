using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageApp.Migrations
{
    public partial class addedNamePropertyToImageServerAndImageDatabaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ImageServer",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ImageDatabase",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ImageServer");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ImageDatabase");
        }
    }
}
