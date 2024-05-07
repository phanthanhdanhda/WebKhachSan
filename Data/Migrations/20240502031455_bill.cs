using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCK.Migrations
{
    /// <inheritdoc />
    public partial class bill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Forms");
        }
    }
}
