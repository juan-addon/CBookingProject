using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CBookingProject.Data.Entities
{
    public class BookingStatus
    {
        public int BookingStatusId { get; set; }

        [Display(Name = "Status Description")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string StatusDescription { get; set; }
        [Display(Name = "Status Code")]
        [MaxLength(10, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string StatusCode { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        public ICollection<Booking> Bookings { get; set; }
    }
}
