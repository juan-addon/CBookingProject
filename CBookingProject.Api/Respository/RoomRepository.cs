using CBookingProject.API.Models;
using CBookingProject.API.Services;
using CBookingProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Repository
{
    public class RoomRepository : IRoomAvailabilityService
    {

        private readonly DataContext _context;
        public RoomRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Response> CheckAvailabilityInDate(DateTime FromDate, DateTime DateTo)
        {
            TimeSpan Diff_dates = FromDate.Date.Subtract(DateTime.Now.Date);
            TimeSpan DiffDateAvailability = DateTo.AddDays(1).AddSeconds(-1).Subtract(FromDate);
            int Maxdays = (int)Math.Abs(Math.Round(DiffDateAvailability.TotalDays));

            try {

                var quantity = this.QuantityBooking(FromDate, DateTo, 1, 1);


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

        public async Task<Response> QuantityBooking(DateTime FromDate, DateTime DateTo, int AvailabilityId, int RoomId)
        {
            TimeSpan Diff_dates = FromDate.Date.Subtract(DateTime.Now.Date);
            TimeSpan DiffDateAvailability = DateTo.AddDays(1).AddSeconds(-1).Subtract(FromDate);
            int Maxdays = (int)Math.Abs(Math.Round(DiffDateAvailability.TotalDays));

            try
            {
                var BookingQuantity = await _context.Bookings
                      .Include(x => x.Rooms)
                      .Join(_context.RoomTypes,
                        quantity => quantity.Rooms.RoomTypeId,
                        roomtype => roomtype.Id,
                        (quantity, roomtype) => new
                        {
                            AvailabilityId = quantity.RoomAvailabilityId,
                            DateFrom = quantity.DateFrom,
                            DateTo = quantity.DateTo,
                            RoomType = roomtype.RoomDescription,
                            RoomName = quantity.Rooms.RoomName,
                            PeopleCapacity = quantity.Rooms.PeopleCapacity,
                            RoomId = quantity.RoomId,
                            RoomTypeId = roomtype.Id
                        }
                      ).Where(x => 
                      (x.DateFrom.Date <= FromDate.Date && x.DateTo.Date >= DateTo.Date) 
                      ||
                      (x.DateTo.Date <= FromDate.Date && x.DateFrom.Date >= DateTo.Date)
                      && x.AvailabilityId == AvailabilityId 
                      && x.RoomId == RoomId
                      ).ToListAsync();



                if (BookingQuantity.Count() == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Availability was not found with the entered parameters."
                    };
                }
                else
                {
                    int RoomTyp = BookingQuantity.FirstOrDefault().RoomTypeId;

                    var Availability = await _context.RoomAvailabilities
                      .Include(x => x.RoomType).Where(x => x.DateFrom.Date <= FromDate.Date && x.DateTo.Date >= DateTo.Date
                        && Diff_dates.Days <= x.MaximumAdvanceReservatio && Diff_dates.Days >= x.MinimumAdvanceReservation
                        && Maxdays <= x.MaxDayAllowed && x.RoomTypeId == RoomTyp
                      ).ToListAsync();


                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Success",
                        Result = BookingQuantity
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
