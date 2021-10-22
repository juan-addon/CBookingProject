using Microsoft.EntityFrameworkCore.Migrations;

namespace CBookingProject.API.Migrations
{
    public partial class addIndexToGuestTableAndBookingStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BookingStatus_BookingStatusId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Guest_GuestId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Rooms_RoomId",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingStatus",
                table: "BookingStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameTable(
                name: "BookingStatus",
                newName: "BookingStatuses");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_RoomId",
                table: "Bookings",
                newName: "IX_Bookings_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_GuestId",
                table: "Bookings",
                newName: "IX_Bookings_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BookingStatusId",
                table: "Bookings",
                newName: "IX_Bookings_BookingStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "GuestNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingStatuses",
                table: "BookingStatuses",
                column: "BookingStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_GuestEmail",
                table: "Guests",
                column: "GuestEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_Identification",
                table: "Guests",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingStatuses_StatusCode",
                table: "BookingStatuses",
                column: "StatusCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingStatuses_StatusDescription",
                table: "BookingStatuses",
                column: "StatusDescription",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId",
                principalTable: "BookingStatuses",
                principalColumn: "BookingStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "GuestNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_GuestEmail",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_Identification",
                table: "Guests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingStatuses",
                table: "BookingStatuses");

            migrationBuilder.DropIndex(
                name: "IX_BookingStatuses_StatusCode",
                table: "BookingStatuses");

            migrationBuilder.DropIndex(
                name: "IX_BookingStatuses_StatusDescription",
                table: "BookingStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameTable(
                name: "BookingStatuses",
                newName: "BookingStatus");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId",
                table: "Booking",
                newName: "IX_Booking_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_GuestId",
                table: "Booking",
                newName: "IX_Booking_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "Booking",
                newName: "IX_Booking_BookingStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "GuestNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingStatus",
                table: "BookingStatus",
                column: "BookingStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BookingStatus_BookingStatusId",
                table: "Booking",
                column: "BookingStatusId",
                principalTable: "BookingStatus",
                principalColumn: "BookingStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Guest_GuestId",
                table: "Booking",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "GuestNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Rooms_RoomId",
                table: "Booking",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
