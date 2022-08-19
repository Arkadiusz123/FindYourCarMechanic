using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindYourCarMechanic.Migrations
{
    public partial class DeleteRedundantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryOfRepairs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryOfRepairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    MechanicId = table.Column<int>(type: "int", nullable: true),
                    RepairDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryOfRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryOfRepairs_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryOfRepairs_Mechanics_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "Mechanics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfRepairs_CarId",
                table: "HistoryOfRepairs",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfRepairs_MechanicId",
                table: "HistoryOfRepairs",
                column: "MechanicId");
        }
    }
}
