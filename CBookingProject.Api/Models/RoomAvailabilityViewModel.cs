using CBookingProject.API.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.API.Models
{
    public class RoomAvailabilityViewModel
    {
        [Key]
        public int AvailabilityId { get; set; }

        [Display(Name = "Season")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string AvailabilityDescription { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DateFrom(ErrorMessage = "Back date entry not allowed")]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "Date To")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [ForeignKey("RoomTypeAvailabilitydFK")]
        public int RoomTypeId { get; set; }
        public IEnumerable<SelectListItem> RoomTypes { get; set; }
    }
}
