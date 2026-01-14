using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Holiday.Data.Migrations
{
    /// <inheritdoc />
    public partial class tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    ticketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticketDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ticketTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.ticketId);
                    table.ForeignKey(
                        name: "FK_ticket_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ticket_FlightId",
                table: "ticket",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket");
        }
    }
}
