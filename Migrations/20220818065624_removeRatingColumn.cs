using Microsoft.EntityFrameworkCore.Migrations;

namespace FindYourCarMechanic.Migrations
{
    public partial class removeRatingColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Mechanics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Mechanics",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
