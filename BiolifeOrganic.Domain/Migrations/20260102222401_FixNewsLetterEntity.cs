using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiolifeOrganic.Dll.Migrations
{
    /// <inheritdoc />
    public partial class FixNewsLetterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "NewsletterSubscribers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "NewsletterSubscribers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "NewsletterSubscribers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "NewsletterSubscribers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "NewsletterSubscribers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "NewsletterSubscribers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "NewsletterSubscribers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "NewsletterSubscribers");
        }
    }
}
