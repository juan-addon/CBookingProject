using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.WebAdmin.Models
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        [Display(Name = "People Quantity")]
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, 10, ErrorMessage = "The maximum number of people per room must be between 1 and 12")]
        public int PeopleCapacity { get; set; }
        [Display(Name = "Room Quantity")]
        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, 10000, ErrorMessage = "The number of rooms must be between 1 and 1000")]
        public int RoomQuantity { get; set; }
        [Display(Name = "Room Name")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string RoomName { get; set; }
        [Display(Name = "Description")]
        [MaxLength(1000, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string RoomDescription { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "The field {0} is required")]
        [ForeignKey("RoomTypeRoomIdFK")]
        public int RoomTypeId { get; set; }
        public IEnumerable<SelectListItem> RoomTypes { get; set; }
    }
}
