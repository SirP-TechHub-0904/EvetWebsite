using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvetWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class xukhfdxsdj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageKey",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageKey",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Reservations");
        }
    }
}
