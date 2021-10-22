using CBookingProject.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoomType>().HasIndex(x => x.RoomDescription).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(x => x.HotelName).IsUnique();
            modelBuilder.Entity<Hotel>().HasIndex(x => x.Description).IsUnique();

            modelBuilder.Entity<Room>().HasIndex(x => x.RoomName).IsUnique();
        }
    }
}
