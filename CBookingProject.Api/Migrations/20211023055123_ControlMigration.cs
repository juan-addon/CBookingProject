using Microsoft.EntityFrameworkCore.Migrations;

namespace CBookingProject.API.Migrations
{
    public partial class ControlMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomAvailabilities_RoomAvailabilitiesAvailabilityId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomAvailabilitiesAvailabilityId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RoomAvailabilitiesAvailabilityId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "AvailabilityId",
                table: "Bookings",
                newName: "RoomAvailabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomAvailabilityId",
                table: "Bookings",
                newName: "AvailabilityId");

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
    }
}
