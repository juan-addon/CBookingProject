using CBookingProject.API.Data.Entities;
using CBookingProject.API.Models;

namespace CBookingProject.API.Helpers
{
    public interface IConverterHelper
    {
        RoomTypeViewModel ToRoomTypeViewModel(RoomType roomType);
        RoomViewModel ToRoomViewModel(Room room);
    }
}
