using Microsoft.EntityFrameworkCore.Migrations;

namespace CBookingProject.Data.Migrations
{
    public partial class ModifyRoomsAvailabilityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxDayAllowed",
                table: "RoomAvailabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDayAllowed",
                table: "RoomAvailabilities");
        }
    }
}
