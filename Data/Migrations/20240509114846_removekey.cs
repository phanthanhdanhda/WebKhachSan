using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCK.Migrations
{
    /// <inheritdoc />
    public partial class removekey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Forms_FormId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_AspNetUsers_UserId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_UserId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Bills_FormId",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Forms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingFormId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_ApplicationUserId",
                table: "Forms",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BookingFormId",
                table: "Bills",
                column: "BookingFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Forms_BookingFormId",
                table: "Bills",
                column: "BookingFormId",
                principalTable: "Forms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_AspNetUsers_ApplicationUserId",
                table: "Forms",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Forms_BookingFormId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_AspNetUsers_ApplicationUserId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_ApplicationUserId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BookingFormId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "BookingFormId",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Forms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_UserId",
                table: "Forms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_FormId",
                table: "Bills",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Forms_FormId",
                table: "Bills",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_AspNetUsers_UserId",
                table: "Forms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
