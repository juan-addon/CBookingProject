using CBookingProject.API.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CBookingProject.API.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetHotels()
        {
            List<SelectListItem> list = _context.Hotels.Select(x => new SelectListItem
            {
                Text = x.HotelName,
                Value = $"{x.HotelId}"
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de documento....]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetRoomTypes()
        {
            List<SelectListItem> list = _context.RoomTypes.Select(x => new SelectListItem
            {
                Text = x.RoomDescription,
                Value = $"{x.Id}"
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de habitacion....]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetAvailabilitiesList()
        {
            List<SelectListItem> list = _context.RoomAvailabilities.Select(x => new SelectListItem
            {
                Text = x.DateFrom.Date+"--"+x.DateTo.Date+"("+x.AvailabilityDescription+")",
                Value = $"{x.AvailabilityId}"
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de habitacion....]",
                Value = "0"
            });

            return list;
        }


    }
}
