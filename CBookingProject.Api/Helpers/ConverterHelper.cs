using CBookingProject.API.Data;
using CBookingProject.API.Data.Entities;
using CBookingProject.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Helpers
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

    }
}
