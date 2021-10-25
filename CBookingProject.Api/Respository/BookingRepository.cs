using CBookingProject.API.Models;
using CBookingProject.API.Services;
using CBookingProject.Data;
using CBookingProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Repository
{
    public class BookingRepository : IBookingService
    {
        private readonly DataContext _context;
        private readonly IRoomAvailabilityService _availabilityService;
        public BookingRepository(DataContext context, IRoomAvailabilityService availabilityService)
        {
            _context = context;
            _availabilityService = availabilityService;
        }
        /// <summary>
        /// This method allows the web api to add a new reservation, it has the responsibility of verifying if the 
        /// user who submits the reservation exists, if the method does not exist it will proceed to register the 
        /// guest and then register the reservation, in case the guest already exists, the system will request 
        /// the guest's id to assign it to the new reservation.
        /// 
        /// Inserts in this method are done through transactions.
        /// </summary>
        /// <returns>
        /// An object of type Response which will indicate the result of the operation and the arrangement 
        /// of the processed information, in case of error it only returns the status of the transaction and the error
        /// message.
        /// </returns>
        public async Task<Response> AddNewBookingWithGuest(BookingViewModel bookingViewModel)
        {
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Task<Response> Availability = _availabilityService.CheckAvailabilityInDate(bookingViewModel.Bookings.DateFrom, bookingViewModel.Bookings.DateTo);

                    if (Availability.Result.IsSuccess)
                    {

                        Response ResultGuest = AddNewGuest(bookingViewModel);

                        if (ResultGuest.IsSuccess)
                        {
                            Guest InserteredGuest = (Guest)ResultGuest.Result;

                            Response BookingResult = AddNewBooking(bookingViewModel, InserteredGuest.GuestNumber);
                            if (BookingResult.IsSuccess)
                            {
                                transaction.Commit();

                                Booking InserteredBooking = (Booking)BookingResult.Result;

                                return new Response
                                {
                                    IsSuccess = true,
                                    Message = "Reservation completed successfully.",
                                    Result = new
                                    {
                                        GuestNumber = InserteredGuest.GuestNumber,
                                        BookingNumber = InserteredBooking.BookingId,
                                        DateFrom = bookingViewModel.Bookings.DateFrom,
                                        DateTo = bookingViewModel.Bookings.DateTo.AddDays(1).AddSeconds(-1)
                                    }
                                };
                            }
                            else
                            {
                                transaction.Rollback();
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = BookingResult.Message
                                };
                            }


                        }
                        else
                        {
                            transaction.Rollback();
                            return new Response
                            {
                                IsSuccess = false,
                                Message = ResultGuest.Message
                            };
                        }
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = false,
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
        /// <summary>
        /// This method allows the web api to modify reservation, it has the responsibility of verifying if the 
        /// user who submits the request is the owner of the reservation, 
        /// If the user is not the owner of the reservation, the system will not process the request, 
        /// if the user is the owner of the request, the method will communicate with the availability 
        /// check method to verify if there is availability in the modification of the reservation.
        /// 
        /// All Request in this method are done through transactions.
        /// </summary>
        /// <returns>
        /// An object of type Response which will indicate the result of the operation and the arrangement 
        /// of the processed information, in case of error it only returns the status of the transaction and the error
        /// message.
        /// </returns>
        public async Task<Response> ModifyBooking(int BookingId, BookingViewModel bookingViewModel)
        {
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    var Availability = _availabilityService.CheckAvailabilityInDate(bookingViewModel.Bookings.DateFrom, bookingViewModel.Bookings.DateTo);

                    if (Availability.Result.IsSuccess)
                    {
                        int GuestNumber = this.ValidateGuestNumber(bookingViewModel.Guests.Identification, bookingViewModel.Guests.GuestEmail);
                        if (GuestNumber == 0)
                        {
                            transaction.Dispose();
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
                                        DateTo = bookingViewModel.Bookings.DateTo.AddDays(1).AddSeconds(-1)
                                    }
                                };
                            }
                            else
                            {
                                transaction.Rollback();
                                return new Response
                                {
                                    IsSuccess = false,
                                    Message = ModifiedBookingId.Message
                                };
                            }
                        }
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = false,
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
        /// <summary>
        /// This method allows the web api to cancel a reservation, it has the responsibility of verifying if the 
        /// user who submits the request is the owner of the reservation, 
        /// If the user is not the owner of the reservation, the method will not process the request, 
        /// if the user is the owner of the request, the method will Cancel the reservation
        /// 
        /// All Request in this method are done through transactions.
        /// </summary>
        /// <returns>
        /// An object of type Response which will indicate the result of the operation and the arrangement 
        /// of the processed information, in case of error it only returns the status of the transaction and the error
        /// message.
        /// </returns>
        public async Task<Response> CancelBooking(int BookingId, BookingCancelParameters bookingCancelViewModel)
        {
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int GuestNumber = ValidateGuestNumber(bookingCancelViewModel.GuestIdentification, bookingCancelViewModel.GuestEmail);
                    if (GuestNumber == 0)
                    {
                        transaction.Dispose();
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "The guest is not authorized to Cancel this reservation, please contact the hotel."
                        };
                    }
                    else
                    {
                        Response CanceledBooking = SaveCancelBooking(BookingId, bookingCancelViewModel, GuestNumber);

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
                                    GuestNumber = GuestNumber
                                }
                            };
                        }
                        else
                        {
                            transaction.Rollback();
                            return new Response
                            {
                                IsSuccess = false,
                                Message = CanceledBooking.Message
                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    return new Response
                    {
                        IsSuccess = false,
                        Message = e.Message
                    };
                }
            }
        }
        /// <summary>
        /// This method allows users to check active reservations at the hotel 
        /// </summary>
        /// <returns>
        /// An object of type Response which will indicate the result of the operation and the arrangement 
        /// of the processed information, in case of error it only returns the status of the transaction and the error
        /// message.
        /// </returns>
        public async Task<Response> GetBookingsByGuestNumber(int guestNumber, string guestIdentification)
        {
            try
            {
                var bookings = await _context.Bookings
                      .Include(x => x.Rooms).Join(_context.Guests,
                        booking => booking.GuestId,
                        guest => guest.GuestNumber,
                        (booking, guest) => new
                        {
                            GuestNumber = guest.GuestNumber,
                            GuestIdentification = guest.Identification,
                            RoomId = booking.RoomId,
                            RoomDescription = booking.Rooms.RoomDescription,
                            BookingNumber = booking.BookingId,
                            FromDate = booking.DateFrom,
                            ToDate = booking.DateTo.AddDays(1).AddSeconds(-1),
                            BookingStatus = booking.Status
                        }
                      ).Where(x => x.BookingStatus == true && x.GuestNumber == guestNumber && x.GuestIdentification == guestIdentification
                      ).ToListAsync();

                if (bookings.Count() == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "The user entered does not have any reservation in our system.",
                        Result = bookings
                    };
                }
                else
                {

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Success",
                        Result = bookings
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

        /// <summary>
        /// Private methods to provide functionality and iteration with the database.
        /// </summary>
        private Response AddNewGuest(BookingViewModel guest)
        {
            Guest RegisteredGuest = _context.Guests.FirstOrDefault(x => x.Identification == guest.Guests.Identification ||
            x.GuestEmail == guest.Guests.GuestEmail);

            if (RegisteredGuest == null)
            {
                try
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

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "User created successfully.",
                        Result = RegisteredGuest
                    };
                }
                catch (DbUpdateException)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                    };
                }
                catch (Exception)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                    };
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Reservation created successfully.",
                    Result = RegisteredGuest
                };
            }
        }
        private int ValidateGuestNumber(string GuestIdentification, string GuestEmail)
        {
            Guest RegisteredGuest = _context.Guests.FirstOrDefault(x => x.Identification == GuestIdentification ||
            x.GuestEmail == GuestEmail);

            if (RegisteredGuest == null)
            {
                return 0;
            }
            else
            {
                return RegisteredGuest.GuestNumber;
            }
        }
        private Response AddNewBooking(BookingViewModel booking, int GuestId)
        {
            try
            {

                Booking newbooking = new Booking
                {
                    DateFrom = booking.Bookings.DateFrom,
                    DateTo = booking.Bookings.DateTo.AddDays(1).AddSeconds(-1),
                    Status = true,
                    BookingStatusId = 1,
                    RoomId = booking.Bookings.RoomId,
                    GuestId = GuestId,
                    RoomAvailabilityId = booking.Bookings.RoomAvailabilityId,
                };

                _context.Bookings.Add(newbooking);
                _context.SaveChanges();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Reservation created successfully.",
                    Result = newbooking
                };

            }
            catch (DbUpdateException)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }
        }
        private Response SaveModifiedBooking(int BookingId, BookingViewModel booking, int GuestId)
        {
            try
            {

                Booking modfiedBooking = _context.Bookings.Include(x => x.Guests).FirstOrDefault(x => x.GuestId == GuestId &&
                 x.BookingId == BookingId && x.Status == true);

                if (modfiedBooking != null)
                {

                    modfiedBooking.DateFrom = booking.Bookings.DateFrom;
                    modfiedBooking.DateTo = booking.Bookings.DateTo.AddDays(1).AddSeconds(-1);
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
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Oops, It seems that the reservation you want to modify does not exist, please check again or contact the hotel."
                    };
                }
            }
            catch (DbUpdateException)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }

        }
        private Response SaveCancelBooking(int BookingId, BookingCancelParameters booking, int GuestId)
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
            catch (DbUpdateException)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "An error has occurred, please try again, if the problem persists please contact customer service."
                };
            }
            catch (Exception)
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
