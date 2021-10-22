using CBookingProject.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CBookingProject.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAvailability> RoomAvailabilities { get; set; }
        public DbSet<RoomPrice> RoomPrices { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoomType>().HasIndex(x => x.RoomDescription).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(x => x.HotelName).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(x => x.Description).IsUnique();

            modelBuilder.Entity<Room>().HasIndex(x => x.RoomName).IsUnique();

            modelBuilder.Entity<Room>().HasIndex(x => x.RoomName).IsUnique();

            modelBuilder.Entity<RoomPrice>().Property(x => x.UnitPrice).HasPrecision(19, 4);

            modelBuilder.Entity<RoomAvailability>().Property(x => x.DateFrom).HasColumnType("datetime");
            modelBuilder.Entity<RoomAvailability>().Property(x => x.DateTo).HasColumnType("datetime");

            modelBuilder.Entity<Guest>().HasIndex(x => x.Identification).IsUnique();
            modelBuilder.Entity<Guest>().HasIndex(x => x.GuestEmail).IsUnique();

            modelBuilder.Entity<BookingStatus>().HasIndex(x => x.StatusCode).IsUnique();
            modelBuilder.Entity<BookingStatus>().HasIndex(x => x.StatusDescription).IsUnique();

        }
    }
}
