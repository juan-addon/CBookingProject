using CBookingProject.Data.Entities;
using CBookingProject.Data;
using CBookingProject.WebAdmin.Models;

namespace CBookingProject.WebAdmin.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public RoomTypeViewModel ToRoomTypeViewModel(RoomType roomType)
        {
            return new RoomTypeViewModel
            {
                Hotels = _combosHelper.GetHotels(),
                Id =  roomType.Id,
                RoomDescription = roomType.RoomDescription,
                Status = roomType.Status,
                HotelId = roomType.HotelId
            };
        }

        public RoomViewModel ToRoomViewModel(Room room)
        {
            return new RoomViewModel
            {
                RoomTypes = _combosHelper.GetRoomTypes(),
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                RoomQuantity = room.RoomQuantity,
                PeopleCapacity = room.PeopleCapacity,
                RoomDescription = room.RoomDescription,
                RoomTypeId = room.RoomTypeId,
                Status = room.Status
            };
        }

        public RoomAvailabilityViewModel ToRoomAvailabilityViewModel(RoomAvailability availability)
        {
            return new RoomAvailabilityViewModel
            {
                RoomTypes = _combosHelper.GetRoomTypes(),
                DateFrom = availability.DateFrom,
                DateTo = availability.DateTo,
                AvailabilityDescription = availability.AvailabilityDescription,
                AvailabilityId = availability.AvailabilityId,
                RoomTypeId = availability.RoomTypeId,
                Status = availability.Status
            };
        }

        public RoomPriceViewModel ToRoomPriceViewModel(RoomPrice price)
        {
            return new RoomPriceViewModel
            {
                RoomAvailabilities = _combosHelper.GetAvailabilitiesList(),
                UnitPrice = price.UnitPrice,
                RoomAvailabilityId = price.RoomAvailabilityId,
                RoomPriceId = price.RoomPriceId,
                Status = price.Status
            };
        }

    }
}
