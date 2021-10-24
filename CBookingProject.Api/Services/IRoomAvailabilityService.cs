using CBookingProject.API.Models;
using System;
using System.Threading.Tasks;

namespace CBookingProject.API.Services
{
    public interface IRoomAvailabilityService
    {
        public Task<Response> CheckAvailabilityInDate(DateTime FromDate, DateTime DateTo);
        public Task<Response> QuantityBooking(DateTime FromDate, DateTime DateTo, int AvailabilityId, int RoomId);
    }
    
}
