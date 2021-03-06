// <auto-generated />
using System;
using CBookingProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CBookingProject.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211023055123_ControlMigration")]
    partial class ControlMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<int>("RoomAvailabilityId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("BookingId");

                    b.HasIndex("BookingStatusId");

                    b.HasIndex("GuestId");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.BookingStatus", b =>
                {
                    b.Property<int>("BookingStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("StatusDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BookingStatusId");

                    b.HasIndex("StatusCode")
                        .IsUnique();

                    b.HasIndex("StatusDescription")
                        .IsUnique();

                    b.ToTable("BookingStatuses");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Guest", b =>
                {
                    b.Property<int>("GuestNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("GuestDateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("GuestEmail")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("GuestLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GuestName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("GuestStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GuestNumber");

                    b.HasIndex("GuestEmail")
                        .IsUnique();

                    b.HasIndex("Identification")
                        .IsUnique();

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("HotelId");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("HotelName")
                        .IsUnique();

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PeopleCapacity")
                        .HasColumnType("int");

                    b.Property<string>("RoomDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RoomQuantity")
                        .HasColumnType("int");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("RoomId");

                    b.HasIndex("RoomName")
                        .IsUnique();

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomAvailability", b =>
                {
                    b.Property<int>("AvailabilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvailabilityDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AvailabilityId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("RoomAvailabilities");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomPrice", b =>
                {
                    b.Property<int>("RoomPriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoomAvailabilityId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.HasKey("RoomPriceId");

                    b.HasIndex("RoomAvailabilityId");

                    b.ToTable("RoomPrices");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("RoomDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomDescription")
                        .IsUnique();

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Booking", b =>
                {
                    b.HasOne("CBookingProject.API.Data.Entities.BookingStatus", "BookingStatus")
                        .WithMany("Bookings")
                        .HasForeignKey("BookingStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CBookingProject.API.Data.Entities.Guest", "Guests")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CBookingProject.API.Data.Entities.Room", "Rooms")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingStatus");

                    b.Navigation("Guests");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Room", b =>
                {
                    b.HasOne("CBookingProject.API.Data.Entities.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomAvailability", b =>
                {
                    b.HasOne("CBookingProject.API.Data.Entities.RoomType", "RoomType")
                        .WithMany("RoomAvailabilities")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomPrice", b =>
                {
                    b.HasOne("CBookingProject.API.Data.Entities.RoomAvailability", "RoomAvailabilities")
                        .WithMany("RoomPrices")
                        .HasForeignKey("RoomAvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomAvailabilities");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomType", b =>
                {
                    b.HasOne("CBookingProject.API.Data.Entities.Hotel", "Hotel")
                        .WithMany("RoomTypes")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.BookingStatus", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Guest", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Hotel", b =>
                {
                    b.Navigation("RoomTypes");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.Room", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomAvailability", b =>
                {
                    b.Navigation("RoomPrices");
                });

            modelBuilder.Entity("CBookingProject.API.Data.Entities.RoomType", b =>
                {
                    b.Navigation("RoomAvailabilities");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
