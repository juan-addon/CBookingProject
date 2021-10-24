using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CBookingProject.Data.Entities
{
    public class Hotel
    {
        public int HotelId { get; set; }
        [Display(Name = "Hotel Name")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string HotelName { get; set; }
        [Display(Name = "Email")]
        [MaxLength(120, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [Display(Name = "Address")]
        [MaxLength(500, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Address { get; set; }
        [Display(Name = "City")]
        [MaxLength(100, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string City { get; set; }
        [Display(Name = "Description")]
        [MaxLength(1000, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Description { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; }
        public ICollection<RoomType> RoomTypes { get; set; }
    }
}
