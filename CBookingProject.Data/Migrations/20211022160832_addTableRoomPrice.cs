using Microsoft.EntityFrameworkCore.Migrations;

namespace CBookingProject.Data.Migrations
{
    public partial class addTableRoomPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomPrices",
                columns: table => new
                {
                    RoomPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    RoomAvailabilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPrices", x => x.RoomPriceId);
                    table.ForeignKey(
                        name: "FK_RoomPrices_RoomAvailabilities_RoomAvailabilityId",
                        column: x => x.RoomAvailabilityId,
                        principalTable: "RoomAvailabilities",
                        principalColumn: "AvailabilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomPrices_RoomAvailabilityId",
                table: "RoomPrices",
                column: "RoomAvailabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomPrices");
        }
    }
}
