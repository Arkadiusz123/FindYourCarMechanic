using Microsoft.EntityFrameworkCore.Migrations;

namespace FindYourCarMechanic.Migrations
{
    public partial class CarToUserConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_UserId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cars",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                newName: "IX_Cars_IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_IdentityUserId",
                table: "Cars",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_IdentityUserId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Cars",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_IdentityUserId",
                table: "Cars",
                newName: "IX_Cars_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_UserId",
                table: "Cars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
