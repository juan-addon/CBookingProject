using CBookingProject.API.Models;
using System;
using System.Threading.Tasks;

namespace CBookingProject.API.Services
{
    public interface IRoomAvailabilityService
    {
        public Task<Response> CheckAvailability(DateTime FromDate, DateTime DateTo);
    }
}
