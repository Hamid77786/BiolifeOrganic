using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiolifeOrganic.Dll.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyForProductIsRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRelated",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRelated",
                table: "Products");
        }
    }
}
