using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Data.Entities
{
    public class RoomPrice
    {
        public int RoomPriceId { get; set; }
        [DisplayName("Price")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal UnitPrice { get; set; }
        public decimal Rate { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomTypeAvailabilityId { get; set; }
        public bool Status { get; set; }
    }
}
