using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CBookingProject.API.Data.Entities
{
    public class Guest
    {
        [Key]
        public int GuestNumber { get; set; }
        [Display(Name = "Identification")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Identification { get; set; }
        [Display(Name = "Guest Name")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string GuestName { get; set; }
        [Display(Name = "Guest LastName")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string GuestLastName { get; set; }
        [Display(Name = "Email")]
        [MaxLength(120, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string GuestEmail { get; set; }
        [Display(Name = "Date From")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime GuestDateOfBirth { get; set; }
        [DefaultValue(true)]
        public bool GuestStatus { get; set; } = true;
        public ICollection<Booking> Bookings { get; set; }
    }
}
