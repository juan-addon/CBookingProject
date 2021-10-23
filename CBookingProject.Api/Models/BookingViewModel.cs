using CBookingProject.API.Data.Entities;

namespace CBookingProject.API.Models
{
    public class BookingViewModel
    {
        public Booking Bookings { get; set; }
        public Guest Guests { get; set; }
    }
}
