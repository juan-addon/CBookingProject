using CBookingProject.API.Data;
using CBookingProject.API.Data.Entities;
using CBookingProject.API.Helpers;
using CBookingProject.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Controllers.API
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        public BookingController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }


        [HttpGet]
        public async Task<IActionResult> CheckAvailability(string FromDate, string ToDate)
        {
            DateTime fromDate;
            DateTime toDate;

            if (DateTime.TryParse(FromDate, out fromDate) && DateTime.TryParse(ToDate, out toDate))
            {
                var availability = await _context.RoomAvailabilities
                  .Include(x => x.RoomType)
                  .Join(_context.Rooms, 
                    availability => availability.RoomTypeId,
                    rooms => rooms.RoomTypeId,
                    (availability, rooms) => new {
                        AvailabilityId = availability.AvailabilityId,
                        AvailabilityDescription = availability.AvailabilityDescription,
                        DateFrom = availability.DateFrom,
                        DateTo = availability.DateTo,
                        RoomType = availability.RoomType.RoomDescription,
                        RoomName = rooms.RoomName,
                        PeopleCapacity = rooms.PeopleCapacity,
                        RoomId = rooms.RoomId
                    }
                  ).Join(_context.RoomPrices,
                    availability => availability.AvailabilityId,
                    roomPrice => roomPrice.RoomAvailabilityId,
                    (availability, roomPrice)=> new {
                        AvailabilityId = availability.AvailabilityId,
                        AvailabilityDescription = availability.AvailabilityDescription,
                        DateFrom = availability.DateFrom,
                        DateTo = availability.DateTo,
                        RoomType = availability.AvailabilityDescription,
                        RoomName = availability.RoomName,
                        PeopleCapacity = availability.PeopleCapacity,
                        RoomId = availability.RoomId,
                        RoomPrice = roomPrice.UnitPrice
                    })
                  .Where(x=> x.DateFrom.Date >= fromDate.Date && x.DateTo.Date <= toDate.Date).ToListAsync();

                if (availability == null)
                {
                    return NotFound();
                }

                return Ok(availability);

            }
            else {
                string error = "Unable to parse FromDate or ToDate." ;
                return BadRequest(error);
            }
        }


        // POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> SaveBooking(BookingViewModel booking)
        {
            await this.SaveGuest(booking.Guests);
            return Ok(booking);
        }

        public async Task<IActionResult> SaveGuest(Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guest RegisteredGuest = _context.Guests.FirstOrDefault(x => x.Identification == guest.Identification ||
            x.GuestEmail == guest.GuestEmail);

            if (RegisteredGuest == null)
            {
                RegisteredGuest = new Guest
                {
                    Identification = guest.Identification,
                    GuestName = guest.GuestName,
                    GuestLastName = guest.GuestLastName,
                    GuestEmail = guest.GuestEmail,
                    GuestDateOfBirth = guest.GuestDateOfBirth,
                    GuestStatus = true
                };

                _context.Guests.Add(RegisteredGuest);
                await _context.SaveChangesAsync();
                return Ok(RegisteredGuest);
            }
            else {
                return Ok(RegisteredGuest);
            }
        }


        /*
                 [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(VehicleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleType vehicleType = await _context.VehicleTypes.FindAsync(request.VehicleTypeId);
            if (vehicleType == null)
            {
                return BadRequest("El tipo de vehículo no existe.");
            }

            Brand brand = await _context.Brands.FindAsync(request.BrandId);
            if (brand == null)
            {
                return BadRequest("La marca no existe.");
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(request.UserId));
            if (user == null)
            {
                return BadRequest("El usuario no existe.");
            }

            Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Plaque.ToUpper() == request.Plaque.ToUpper());
            if (vehicle != null)
            {
                return BadRequest("Ya existe un vehículo con esa placa.");
            }

            Guid imageId = Guid.Empty;
            List<VehiclePhoto> vehiclePhotos = new();
            if (request.Image != null && request.Image.Length > 0)
            {
                imageId = await _blobHelper.UploadBlobAsync(request.Image, "vehiclephotos");
                vehiclePhotos.Add(new VehiclePhoto
                {
                    ImageId = imageId
                });
            }

            vehicle = new Vehicle
            {
                Brand = brand,
                Color = request.Color,
                Histories = new List<History>(),
                Line = request.Line,
                Model = request.Model,
                Plaque = request.Plaque,
                Remarks = request.Remarks,
                User = user,
                VehiclePhotos = vehiclePhotos,
                VehicleType = vehicleType,
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return Ok(vehicle);
        }
         
         */

    }
}
