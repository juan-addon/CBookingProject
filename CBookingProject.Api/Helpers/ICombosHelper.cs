using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CBookingProject.API.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetHotels();
        IEnumerable<SelectListItem> GetRoomTypes();
        IEnumerable<SelectListItem> GetAvailabilitiesList();
    }
}
