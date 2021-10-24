using CBookingProject.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckHotelsAsync();
            await CheckBookingAsync();
        }

        private async Task CheckHotelsAsync()
        {
            if (!_context.Hotels.Any()) {
                _context.Hotels.Add(new Hotel { HotelName="Hotel Cancum", Address="Test Address", City="Mexico", Description="Hotel", Email="testing@cancum.com", Status=true });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckBookingAsync()
        {
            if (!_context.BookingStatuses.Any())
            {
                _context.BookingStatuses.Add(new BookingStatus 
                { StatusCode="CANCEL", StatusDescription="CANCEL", Status = true });

                _context.BookingStatuses.Add(new BookingStatus
                { StatusCode = "ACTIVE", StatusDescription = "ACTIVE", Status = true });

                await _context.SaveChangesAsync();
            }
        }
    }
}
