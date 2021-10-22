using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.API.Models
{
    public class RoomPriceViewModel
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
        public IEnumerable<SelectListItem> RoomAvailabilities { get; set; }
    }
}
