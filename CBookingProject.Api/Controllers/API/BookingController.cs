using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CBookingProject.API.Controllers.API
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<string> ChecKAvailability()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public IEnumerable<string> PlaceReservation()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IEnumerable<string> ModifyReservation()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IEnumerable<string> CancelReservation()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet]
        public IEnumerable<string> GetMyReservations()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IEnumerable<string> ReservationExists()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
