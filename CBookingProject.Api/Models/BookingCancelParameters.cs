using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Models
{
    public class BookingCancelParameters
    {
        public string GuestIdentification { get; set; }
        public string GuestEmail { get; set; }
    }
}
