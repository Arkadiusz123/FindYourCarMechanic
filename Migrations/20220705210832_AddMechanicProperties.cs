using Microsoft.EntityFrameworkCore.Migrations;

namespace FindYourCarMechanic.Migrations
{
    public partial class AddMechanicProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Mechanics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Mechanics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Mechanics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Mechanics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Mechanics");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Mechanics");
        }
    }
}
