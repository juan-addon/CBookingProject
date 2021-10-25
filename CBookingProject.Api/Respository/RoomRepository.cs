﻿using CBookingProject.API.Models;
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
                            RoomQuantity = rooms.RoomQuantity,
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
                            DateFrom = FromDate.Date,
                            DateTo = DateTo.Date,
                            RoomType = availability.AvailabilityDescription,
                            RoomName = availability.RoomName,
                            PeopleCapacity = availability.PeopleCapacity,
                            RoomId = availability.RoomId,
                            RoomQuantity = availability.RoomQuantity,
                            MinimumAdvanceReservation = availability.MinimumAdvanceReservation,
                            MaximumAdvanceReservatio = availability.MaximumAdvanceReservatio,
                            MaxDayAllowed = availability.MaxDayAllowed,
                            RoomPrice = roomPrice.UnitPrice,
                            BookingQuantity = (from c in _context.Bookings.Include(m => m.Rooms)
                                                       where c.Status == true && (c.DateFrom.Date <= FromDate.Date && c.DateTo.Date >= DateTo.Date)
                                                         || (c.DateTo.Date <= FromDate.Date && c.DateFrom.Date >= DateTo.Date) && c.RoomAvailabilityId == availability.AvailabilityId
                                                         && c.RoomId == availability.RoomId
                                                       select new { c.BookingId }).Distinct().Count()
                        })
                      .Where(x => x.DateFrom.Date <= FromDate.Date && x.DateTo.Date >= DateTo.Date
                        && Diff_dates.Days <= x.MaximumAdvanceReservatio && Diff_dates.Days >= x.MinimumAdvanceReservation
                        && Maxdays <= x.MaxDayAllowed && x.BookingQuantity < x.RoomQuantity
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
