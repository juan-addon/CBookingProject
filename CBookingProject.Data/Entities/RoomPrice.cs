using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.Data.Entities
{
    public class RoomPrice
    {
        [Key]
        public int RoomPriceId { get; set; }
        [DisplayName("Price")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal UnitPrice { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;

        [Display(Name = "Availability Description")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [ForeignKey("RoomAvailabilityPriceFK")]
        public int RoomAvailabilityId { get; set; }
        public RoomAvailability RoomAvailabilities { get; set; }
    }
}
