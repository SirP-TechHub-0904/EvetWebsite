using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvetWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class xukhfdxsdjjd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "Reservations");
        }
    }
}
