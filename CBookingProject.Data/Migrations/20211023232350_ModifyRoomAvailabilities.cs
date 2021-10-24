using Microsoft.EntityFrameworkCore.Migrations;

namespace CBookingProject.Data.Migrations
{
    public partial class ModifyRoomAvailabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumAdvanceReservatio",
                table: "RoomAvailabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumAdvanceReservation",
                table: "RoomAvailabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumAdvanceReservatio",
                table: "RoomAvailabilities");

            migrationBuilder.DropColumn(
                name: "MinimumAdvanceReservation",
                table: "RoomAvailabilities");
        }
    }
}
