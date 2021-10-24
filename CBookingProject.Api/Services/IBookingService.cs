using CBookingProject.API.Models;
using System.Threading.Tasks;

namespace CBookingProject.API.Services
{
    public interface IBookingService
    {
        public Task<Response> AddNewBookingWithGuest(BookingViewModel bookingViewModel);
        public Task<Response> ModifyBooking(int BookingId, BookingViewModel bookingViewModel);
        public Task<Response> CancelBooking(int BookingId, BookingViewModel bookingViewModel);
    }
}
