using CBookingProject.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.WebAdmin.Models
{
    public class RoomAvailabilityViewModel
    {
        [Key]
        public int AvailabilityId { get; set; }

        [Display(Name = "Season")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string AvailabilityDescription { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "Date To")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Minimum Advance reservations(Days)")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int MinimumAdvanceReservation { get; set; }
        [Display(Name = "Maximum Advance reservations(Days)")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int MaximumAdvanceReservatio { get; set; }
        [Display(Name = "Max Allowed Days")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int MaxDayAllowed { get; set; }
        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "The field {0} is required")]
        [ForeignKey("RoomTypeAvailabilitydFK")]
        public int RoomTypeId { get; set; }
        public IEnumerable<SelectListItem> RoomTypes { get; set; }
    }
}
