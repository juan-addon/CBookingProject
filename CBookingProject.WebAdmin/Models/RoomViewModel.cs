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
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(1, 10, ErrorMessage = "Personas maximas de 0 a 12")]
        public int PeopleCapacity { get; set; }
        [Display(Name = "Room Quantity")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Range(1, 10000, ErrorMessage = "Cantidad de habitaciones de 1 a 1000")]
        public int RoomQuantity { get; set; }
        [Display(Name = "Room Name")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string RoomName { get; set; }
        [Display(Name = "Description")]
        [MaxLength(1000, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string RoomDescription { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [ForeignKey("RoomTypeRoomIdFK")]
        public int RoomTypeId { get; set; }
        public IEnumerable<SelectListItem> RoomTypes { get; set; }
    }
}
