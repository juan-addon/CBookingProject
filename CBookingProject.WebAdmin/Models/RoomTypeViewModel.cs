using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CBookingProject.WebAdmin.Models
{
    public class RoomTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Room Type")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string RoomDescription { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Hotel")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select the hotel.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int HotelId { get; set; }
        public IEnumerable<SelectListItem> Hotels { get; set; }
    }
}
