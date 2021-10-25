using System;

namespace CBookingProject.API.Models
{
    public class BookingResult
    {
        public int AvailabilityId { get; set; }
        public string AvailabilityDescription { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateFromRequested { get; set; }
        public DateTime DateToRequested { get; set; }
        public string RoomType { get; set; }
        public string RoomName { get; set; }
        public int PeopleCapacity { get; set; }
        public int RoomId { get; set; }
        public decimal RoomPrice { get; set; }
        public int MinimumAdvanceReservation { get; set; }
        public int MaximumAdvanceReservatio { get; set; }
        public int MaxDayAllowed { get; set; }
        public int RoomQuantity { get; set; }
        public int BookingQuantity { get; set; }

    }
}
