using System;

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
        public DateTime GuestDateOfBirth { get; set; }
    }

    public class NewBooking
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BookingStatusId { get; set; }
        public int RoomId { get; set; }
        public int RoomAvailabilityId { get; set; }
    }

}
