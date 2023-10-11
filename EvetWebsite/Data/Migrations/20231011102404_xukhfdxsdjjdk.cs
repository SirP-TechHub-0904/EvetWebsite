using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvetWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class xukhfdxsdjjdk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BirthdayMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BirthdayMessages");
        }
    }
}
