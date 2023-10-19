using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvetWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class dddddddddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayImage",
                table: "Committees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageKey",
                table: "Committees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Committees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayImage",
                table: "CommitteeCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayImage",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "ImageKey",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "DisplayImage",
                table: "CommitteeCategories");
        }
    }
}
