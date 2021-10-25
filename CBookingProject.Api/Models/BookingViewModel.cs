using System;
using System.ComponentModel.DataAnnotations;

namespace CBookingProject.API.Models
{
    public class BookingViewModel
    {
        public NewBooking Bookings { get; set; }
        public NewGuest Guests { get; set; }
    }

    public class NewGuest
    {
        public string Identification { get; set; }
        public string GuestName { get; set; }
        public string GuestLastName { get; set; }
        public string GuestEmail { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime GuestDateOfBirth { get; set; }
    }

    public class NewBooking
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        public int RoomId { get; set; }
        public int RoomAvailabilityId { get; set; }
    }

}
