using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiolifeOrganic.Dll.Migrations
{
    /// <inheritdoc />
    public partial class EditedSliderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "LinkUrl",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Sliders");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Sliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HtmlContent",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "HtmlContent",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sliders");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkUrl",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
