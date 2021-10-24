using CBookingProject.Data.Entities;
using CBookingProject.WebAdmin.Models;

namespace CBookingProject.WebAdmin.Helpers
{
    public interface IConverterHelper
    {
        RoomTypeViewModel ToRoomTypeViewModel(RoomType roomType);
        RoomViewModel ToRoomViewModel(Room room);
        RoomAvailabilityViewModel ToRoomAvailabilityViewModel(RoomAvailability availability);
        RoomPriceViewModel ToRoomPriceViewModel(RoomPrice availability);
    }
}
