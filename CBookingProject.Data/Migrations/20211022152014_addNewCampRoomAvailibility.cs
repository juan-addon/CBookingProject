using Microsoft.EntityFrameworkCore.Migrations;

namespace CBookingProject.Data.Migrations
{
    public partial class addNewCampRoomAvailibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAvailability_RoomTypes_RoomTypeId",
                table: "RoomAvailability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomAvailability",
                table: "RoomAvailability");

            migrationBuilder.RenameTable(
                name: "RoomAvailability",
                newName: "RoomAvailabilities");

            migrationBuilder.RenameIndex(
                name: "IX_RoomAvailability_RoomTypeId",
                table: "RoomAvailabilities",
                newName: "IX_RoomAvailabilities_RoomTypeId");

            migrationBuilder.AddColumn<string>(
                name: "AvailabilityDescription",
                table: "RoomAvailabilities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomAvailabilities",
                table: "RoomAvailabilities",
                column: "AvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAvailabilities_RoomTypes_RoomTypeId",
                table: "RoomAvailabilities",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAvailabilities_RoomTypes_RoomTypeId",
                table: "RoomAvailabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomAvailabilities",
                table: "RoomAvailabilities");

            migrationBuilder.DropColumn(
                name: "AvailabilityDescription",
                table: "RoomAvailabilities");

            migrationBuilder.RenameTable(
                name: "RoomAvailabilities",
                newName: "RoomAvailability");

            migrationBuilder.RenameIndex(
                name: "IX_RoomAvailabilities_RoomTypeId",
                table: "RoomAvailability",
                newName: "IX_RoomAvailability_RoomTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomAvailability",
                table: "RoomAvailability",
                column: "AvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAvailability_RoomTypes_RoomTypeId",
                table: "RoomAvailability",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
