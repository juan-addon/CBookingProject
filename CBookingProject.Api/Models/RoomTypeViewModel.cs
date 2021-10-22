using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Models
{
    public class RoomTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Room Type")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string RoomDescription { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Hotel")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un Hotel.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int HotelId { get; set; }
        public IEnumerable<SelectListItem> Hotels { get; set; }
    }
}
