using Microsoft.EntityFrameworkCore.Migrations;

namespace CBookingProject.API.Migrations
{
    public partial class ModifyBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailabilityId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomAvailabilitiesAvailabilityId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomAvailabilitiesAvailabilityId",
                table: "Bookings",
                column: "RoomAvailabilitiesAvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_RoomAvailabilities_RoomAvailabilitiesAvailabilityId",
                table: "Bookings",
                column: "RoomAvailabilitiesAvailabilityId",
                principalTable: "RoomAvailabilities",
                principalColumn: "AvailabilityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomAvailabilities_RoomAvailabilitiesAvailabilityId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomAvailabilitiesAvailabilityId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AvailabilityId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RoomAvailabilitiesAvailabilityId",
                table: "Bookings");
        }
    }
}
