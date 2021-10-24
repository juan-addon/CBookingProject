using CBookingProject.API.Models;
using CBookingProject.API.Services;
using CBookingProject.Data;
using CBookingProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Repository {
    public class BookingRepository : IBookingService
    {
        private readonly DataContext _context;
        private readonly IRoomAvailabilityService _availabilityService;
        public BookingRepository(DataContext context, IRoomAvailabilityService availabilityService)
        {
            _context = context;
            _availabilityService = availabilityService;
        }

        public async Task<Response> AddNewBookingWithGuest(BookingViewModel bookingViewModel)
    {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var Availability = _availabilityService.CheckAvailabilityInDate(bookingViewModel.Bookings.DateFrom, bookingViewModel.Bookings.DateTo);

                    if (Availability.Result.IsSuccess)
                    {

                        int GuestNumber = this.AddNewGuest(bookingViewModel);
                        int BookingNumber = this.AddNewBooking(bookingViewModel, GuestNumber);

                        transaction.Commit();

                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Reservation completed successfully.",
                            Result = new {
                                GuestNumber= GuestNumber,
                                BookingNumber= BookingNumber,
                                DateFrom = bookingViewModel.Bookings.DateFrom,
                                DateTo =  bookingViewModel.Bookings.DateTo
                            }
                        };
                    }
                    else {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Oops, it seems that the room you want to reserve does not have availability for the indicated dates."
                        };
                    }
                }
                catch (Exception e)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = e.Message
                    };
                }
            }
    }
        public async Task<Response> ModifyBooking(int BookingId, BookingViewModel bookingViewModel)
        {
                using (var transaction = _context.Database.BeginTransaction())
                {

                    var Availability = _availabilityService.CheckAvailabilityInDate(bookingViewModel.Bookings.DateFrom, bookingViewModel.Bookings.DateTo);

                    if (Availability.Result.IsSuccess)
                    {
                        int GuestNumber = this.ValidateGuestNumber(bookingViewModel);
                        if (GuestNumber == 0)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = "The guest is not authorized to modify this reservation, please contact the hotel."
                            };
                        }
                        else
                        {
                            var ModifiedBookingId = this.SaveModifiedBooking(BookingId, bookingViewModel, GuestNumber);

                        if (ModifiedBookingId.IsSuccess)
                        {
                            transaction.Commit();

                            return new Response
                            {
                                IsSuccess = true,
                                Message = "Reservation satisfactorily modified.",
                                Result = new
                                {
                                    ModifiedBookingId = ModifiedBookingId.Result,
                                    GuestNumber = GuestNumber,
                                    DateFrom = bookingViewModel.Bookings.DateFrom,
                                    DateTo = bookingViewModel.Bookings.DateTo
                                }
                            };
                        }
                        else {
                            return ModifiedBookingId;
                        }
                        }
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Oops, it seems that the room you want to reserve does not have availability for the indicated dates."
                        };
                    }
            }
        }
        public async Task<Response> CancelBooking(int BookingId, BookingViewModel bookingViewModel)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {

                var Availability = _availabilityService.CheckAvailabilityInDate(bookingViewModel.Bookings.DateFrom, bookingViewModel.Bookings.DateTo);

                if (Availability.Result.IsSuccess)
                {
                    int GuestNumber = this.ValidateGuestNumber(bookingViewModel);
                    if (GuestNumber == 0)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "The guest is not authorized to Cancel this reservation, please contact the hotel."
                        };
                    }
                    else
                    {
                        var CanceledBooking = this.SaveCancelBooking(BookingId, bookingViewModel, GuestNumber);

                        if (CanceledBooking.IsSuccess)
                        {
                            transaction.Commit();

                            return new Response
                            {
                                IsSuccess = true,
                                Message = "Reservation Canceled.",
                                Result = new
                                {
                                    ModifiedBookingId = CanceledBooking.Result,
                                    GuestNumber = GuestNumber,
                                    DateFrom = bookingViewModel.Bookings.DateFrom,
                                    DateTo = bookingViewModel.Bookings.DateTo
                                }
                            };
                        }
                        else
                        {
                            return CanceledBooking;
                        }
                    }
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                    };
                }
            }
        }



        /// <summary>
        /// Private methods to provide functionality and iteration with the database.
        /// </summary>
        private int AddNewGuest(BookingViewModel guest)
        {
            Guest RegisteredGuest = _context.Guests.FirstOrDefault(x => x.Identification == guest.Guests.Identification ||
            x.GuestEmail == guest.Guests.GuestEmail);

            if (RegisteredGuest == null)
            {
                RegisteredGuest = new Guest
                {
                    Identification = guest.Guests.Identification,
                    GuestName = guest.Guests.GuestName,
                    GuestLastName = guest.Guests.GuestLastName,
                    GuestEmail = guest.Guests.GuestEmail,
                    GuestDateOfBirth = guest.Guests.GuestDateOfBirth,
                    GuestStatus = true
                };

                _context.Guests.Add(RegisteredGuest);
                _context.SaveChanges();

                return RegisteredGuest.GuestNumber;
            }
            else {
                return RegisteredGuest.GuestNumber;
            }
        }
        private int ValidateGuestNumber(BookingViewModel guest)
        {
            Guest RegisteredGuest = _context.Guests.FirstOrDefault(x => x.Identification == guest.Guests.Identification ||
            x.GuestEmail == guest.Guests.GuestEmail);

            if (RegisteredGuest == null)
            {
                return 0;
            }
            else
            {
                return RegisteredGuest.GuestNumber;
            }
        }
        private int AddNewBooking(BookingViewModel booking, int GuestId)
        {
                try
                {

                    Booking newbooking = new Booking
                    {
                        DateFrom = booking.Bookings.DateFrom,
                        DateTo = booking.Bookings.DateTo,
                        Status = true,
                        BookingStatusId = 1,
                        RoomId = booking.Bookings.RoomId,
                        GuestId = GuestId,
                        RoomAvailabilityId = booking.Bookings.RoomAvailabilityId,
                    };

                    _context.Bookings.Add(newbooking);
                    _context.SaveChanges();

                    return newbooking.BookingId;
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        //ModelState.AddModelError(string.Empty, "Ya existe este Hotel");
                    }
                    else
                    {
                        //ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    return 0;
                    //ModelState.AddModelError(string.Empty, ex.Message);
                }

        }
        private Response SaveModifiedBooking(int BookingId,BookingViewModel booking, int GuestId)
        {
            try
            {

                Booking modfiedBooking = _context.Bookings.Include(x=> x.Guests).FirstOrDefault(x => x.GuestId == GuestId &&
                x.BookingId == BookingId && x.Status == true);

                if (modfiedBooking != null)
                {

                    modfiedBooking.DateFrom = booking.Bookings.DateFrom;
                    modfiedBooking.DateTo = booking.Bookings.DateTo;
                    modfiedBooking.Status = true;
                    modfiedBooking.BookingStatusId = 1;
                    modfiedBooking.RoomId = booking.Bookings.RoomId;
                    modfiedBooking.RoomAvailabilityId = booking.Bookings.RoomAvailabilityId;
    
                    _context.Bookings.Update(modfiedBooking);
                    _context.SaveChanges();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Reservation satisfactorily modified.",
                        Result = modfiedBooking.BookingId
                    };

                }
                else {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Oops, It seems that the reservation you want to modify does not exist, please check again or contact the hotel."
                    };
                }
            }
            catch (DbUpdateException db)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }

        }
        private Response SaveCancelBooking(int BookingId, BookingViewModel booking, int GuestId)
        {
            try
            {
                Booking modfiedBooking = _context.Bookings.Include(x => x.Guests).FirstOrDefault(x => x.GuestId == GuestId &&
                 x.BookingId == BookingId && x.Status == true);

                if (modfiedBooking != null)
                {
                    modfiedBooking.Status = false;
                    modfiedBooking.BookingStatusId = 2;

                    _context.Bookings.Update(modfiedBooking);
                    _context.SaveChanges();

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Reservation Canceled",
                        Result = modfiedBooking.BookingId
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Oops, It seems that the reservation you want to cancel does not exist, please check again or contact the hotel."
                    };
                }
            }
            catch (DbUpdateException db)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }
        }
    }
}
