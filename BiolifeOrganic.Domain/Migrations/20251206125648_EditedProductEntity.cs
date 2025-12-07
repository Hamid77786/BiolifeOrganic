using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiolifeOrganic.Dll.Migrations
{
    /// <inheritdoc />
    public partial class EditedProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInWishlist",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInWishlist",
                table: "Products");
        }
    }
}
