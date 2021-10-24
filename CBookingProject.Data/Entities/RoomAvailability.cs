using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.Data.Entities
{
    public class RoomAvailability
    {
        [Key]
        public int AvailabilityId { get; set; }

        [Display(Name = "Season")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string AvailabilityDescription { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DateFrom(ErrorMessage = "Back date entry not allowed")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Date To")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Minimum Advance reservations(Days)")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int MinimumAdvanceReservation { get; set; }
        [Display(Name = "Maximum Advance reservations(Days)")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int MaximumAdvanceReservatio { get; set; }
        [Display(Name = "Max Allowed Days")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int MaxDayAllowed { get; set; }
        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [ForeignKey("RoomTypeAvailabilitydFK")]
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<RoomPrice> RoomPrices { get; set; }
    }
}
