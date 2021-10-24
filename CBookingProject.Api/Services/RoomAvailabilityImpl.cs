using CBookingProject.API.Models;
using CBookingProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Services
{
    public class RoomAvailabilityImpl : IRoomAvailabilityService
    {

        private readonly DataContext _context;
        public RoomAvailabilityImpl(DataContext context)
        {
            _context = context;
        }
        public async Task<Response> CheckAvailability(DateTime FromDate, DateTime DateTo)
        {
            TimeSpan Diff_dates = FromDate.Date.Subtract(DateTime.Now.Date);
            TimeSpan DiffDateAvailability = DateTo.AddDays(1).AddSeconds(-1).Subtract(FromDate);
            int Maxdays = (int)Math.Abs(Math.Round(DiffDateAvailability.TotalDays));

            try {
                var availability = await _context.RoomAvailabilities
                      .Include(x => x.RoomType)
                      .Join(_context.Rooms,
                        availability => availability.RoomTypeId,
                        rooms => rooms.RoomTypeId,
                        (availability, rooms) => new BookingResult
                        {
                            AvailabilityId = availability.AvailabilityId,
                            AvailabilityDescription = availability.AvailabilityDescription,
                            DateFrom = availability.DateFrom,
                            DateTo = availability.DateTo,
                            RoomType = availability.RoomType.RoomDescription,
                            RoomName = rooms.RoomName,
                            PeopleCapacity = rooms.PeopleCapacity,
                            RoomId = rooms.RoomId,
                            MinimumAdvanceReservation = availability.MinimumAdvanceReservation,
                            MaximumAdvanceReservatio = availability.MaximumAdvanceReservatio,
                            MaxDayAllowed = availability.MaxDayAllowed
                        }
                      ).Join(_context.RoomPrices,
                        availability => availability.AvailabilityId,
                        roomPrice => roomPrice.RoomAvailabilityId,
                        (availability, roomPrice) => new BookingResult
                        {
                            AvailabilityId = availability.AvailabilityId,
                            AvailabilityDescription = availability.AvailabilityDescription,
                            DateFrom = availability.DateFrom,
                            DateTo = availability.DateTo,
                            RoomType = availability.AvailabilityDescription,
                            RoomName = availability.RoomName,
                            PeopleCapacity = availability.PeopleCapacity,
                            RoomId = availability.RoomId,
                            MinimumAdvanceReservation = availability.MinimumAdvanceReservation,
                            MaximumAdvanceReservatio = availability.MaximumAdvanceReservatio,
                            MaxDayAllowed = availability.MaxDayAllowed,
                            RoomPrice = roomPrice.UnitPrice
                        })
                      .Where(x => x.DateFrom.Date <= FromDate.Date && x.DateTo.Date >= DateTo.Date
                        && Diff_dates.Days <= x.MaximumAdvanceReservatio && Diff_dates.Days >= x.MinimumAdvanceReservation
                        && Maxdays <= x.MaxDayAllowed
                      ).ToListAsync();

                if (availability.Count() == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Availability was not found with the entered parameters.",
                        Result = availability
                    };
                }
                else {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Success",
                        Result = availability
                    };
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = dbUpdateException.Message
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
