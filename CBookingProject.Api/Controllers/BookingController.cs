using CBookingProject.API.Models;
using CBookingProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CBookingProject.API.Services;
using System;
using System.Globalization;

namespace CBookingProject.API.Controllers.API
{
    /// <summary>
    /// Descripcion del API
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IRoomAvailabilityService _availabilityService;
        private readonly IBookingService _bookingService;
        public BookingController(DataContext context, IRoomAvailabilityService availabilityService,
            IBookingService bookingService)
        {
            _context = context;
            _availabilityService = availabilityService;
            _bookingService = bookingService;
        }

        /// <summary>
        /// This Get method returns the list of rooms available according to the start and end date entered by the end-user, 
        /// the list of rooms is correct by the rules set by the administrators in the room availability window, for example:
        /// A room can be configured as follows:
        /// Minimum reservation day one day before the stay.
        /// Maximum days allowed to make the reservation 30 days.
        /// Maximum days allowed in the reserved room are 3 days.
        /// </summary>
        /// <returns>An array with the list of available rooms.</returns>
        [HttpPost("CheckAvailability")]
        public IActionResult CheckAvailability([FromQuery] SearchParameters search)
        {
            if (IsDateTime(search.FromDate.ToShortDateString()) && IsDateTime(search.DateTo.ToShortDateString()))
            {
                var result = _availabilityService.CheckAvailabilityInDate(search.FromDate, search.DateTo);

                return Ok(result.Result);
            }
            else {
                return Ok(
                         new Response
                         {
                             IsSuccess = false,
                             Message = "Invalid date parameters."
                         }
                    );
            }

        }



        /// <summary>
        /// This Post method allows end users to make room reservations according to room availability.
        /// </summary>
        /// <returns>
        /// An Array with the status of the save action, if the record is stored successfully 
        /// the array will include the reservation detail
        /// </returns>
        [HttpPost("SaveBooking")]
        public IActionResult SaveBooking([FromQuery] BookingViewModel booking)
        {
            if (IsDateTime(booking.Bookings.DateFrom.ToShortDateString()) && IsDateTime(booking.Bookings.DateTo.ToShortDateString()))
            {
                var result = _bookingService.AddNewBookingWithGuest(booking);
                return Ok(result.Result);
            }
            else
            {
                return Ok(
                         new Response
                         {
                             IsSuccess = false,
                             Message = "Invalid date parameters."
                         }
                    );
            }

        }


        /// <summary>
        /// This Put method allows end users to modify a reservations according to room availability.
        /// </summary>
        /// <returns>
        /// An Array with the status of the save action, if the record is stored successfully 
        /// the array will include the reservation detail
        /// </returns>
        [HttpPut("ModifyBooking/{BookingId}")]
        public IActionResult ModifyBooking(int BookingId, [FromQuery] BookingViewModel booking)
        {

            if (IsDateTime(booking.Bookings.DateFrom.ToShortDateString()) && IsDateTime(booking.Bookings.DateTo.ToShortDateString()))
            {
                var result = _bookingService.ModifyBooking(BookingId, booking);
                return Ok(result.Result);
            }
            else
            {
                return Ok(
                         new Response
                         {
                             IsSuccess = false,
                             Message = "Invalid date parameters."
                         }
                    );
            }
        }

        /// <summary>
        /// This Delete method allows end users to Cancel a reservations.
        /// </summary>
        /// <returns>
        /// An Array with the status of the save action, if the record is stored successfully 
        /// the array will include the reservation detail
        /// </returns>
        [HttpDelete("CancelBooking/{BookingId}")]
        public async Task<IActionResult> CancelBooking(int BookingId, [FromQuery] BookingViewModel booking)
        {
            if (IsDateTime(booking.Bookings.DateFrom.ToShortDateString()) && IsDateTime(booking.Bookings.DateTo.ToShortDateString()))
            {
                var result = _bookingService.CancelBooking(BookingId, booking);
                return Ok(result.Result);
            }
            else
            {
                return Ok(
                         new Response
                         {
                             IsSuccess = false,
                             Message = "Invalid date parameters."
                         }
                    );
            }
        }

        private bool IsDateTime(string date) {

            DateTime dateConverted;

            string pattern = "MM/dd/yyyy";
            CultureInfo enUS = new CultureInfo("en-US");
            if (!DateTime.TryParseExact(date, pattern, enUS,
                                DateTimeStyles.AdjustToUniversal, out dateConverted))
            {
                return false;
            }
            else {
                return true;
            }
        }

    }
}
