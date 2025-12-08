using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiolifeOrganic.Dll.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyForCategoryIsRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRelated",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRelated",
                table: "Categories");
        }
    }
}
