using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvetWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class hgxcjyfdklhyjdfdj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageIv",
                table: "RSVPs");

            migrationBuilder.AddColumn<string>(
                name: "ImageKey",
                table: "RSVPs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "RSVPs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageKey",
                table: "RSVPs");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "RSVPs");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageIv",
                table: "RSVPs",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
