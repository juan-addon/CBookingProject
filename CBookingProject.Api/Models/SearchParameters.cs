using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Models
{
    public class SearchParameters
    {
        public DateTime FromDate { get; set; }
        public DateTime DateTo { get; set; }
    }
}
