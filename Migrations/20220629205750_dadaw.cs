using Microsoft.EntityFrameworkCore.Migrations;

namespace FindYourCarMechanic.Migrations
{
    public partial class dadaw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmedRepairs_AspNetUsers_UserId",
                table: "ConfirmedRepairs");

            migrationBuilder.DropIndex(
                name: "IX_ConfirmedRepairs_UserId",
                table: "ConfirmedRepairs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ConfirmedRepairs");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ConfirmedRepairs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmedRepairs_UserId",
                table: "ConfirmedRepairs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmedRepairs_AspNetUsers_UserId",
                table: "ConfirmedRepairs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
