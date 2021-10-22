using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Data.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        
        [Display(Name = "Date From")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Date To")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public int RoomId { get; set; }
        public Room Rooms { get; set; }
        public int GuestId { get; set; }
        public Guest Guests { get; set; }

    }
}
