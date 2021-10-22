using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Data.Entities
{
    public class BookingStatus
    {
        public int BookingStatusId { get; set; }

        [Display(Name = "Status Description")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string StatusDescription { get; set; }
        [Display(Name = "Status Code")]
        [MaxLength(10, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string StatusCode { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        public ICollection<Booking> Bookings { get; set; }
    }
}
