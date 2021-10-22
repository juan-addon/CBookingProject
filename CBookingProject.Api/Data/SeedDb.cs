using CBookingProject.API.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Data
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
            //await CheckRoomTypesAsync();
        }

        private async Task CheckHotelsAsync()
        {
            if (!_context.Hotels.Any()) {
                _context.Hotels.Add(new Hotel { HotelName="Hotel Cancum", Address="Test Address", City="Mexico", Description="Hotel", Email="testing@cancum.com", Status=true });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRoomTypesAsync()
        {
            if (!_context.RoomTypes.Any())
            {
                _context.RoomTypes.Add(new RoomType { RoomDescription="Twin Room", Status=true });
                await _context.SaveChangesAsync();
            }
        }
    }
}
