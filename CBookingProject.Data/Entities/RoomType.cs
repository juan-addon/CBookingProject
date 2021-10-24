using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.Data.Entities
{
    public class RoomType
    {
        public int Id { get; set; }

        [Display(Name ="Room Type")]
        [MaxLength(50, ErrorMessage = "The field {0} cannot have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required")]
        public string RoomDescription { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Hotel")]
        [Required(ErrorMessage = "The field {0} is required")]
        [ForeignKey("RoomTypeHotelIdFK")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<RoomAvailability> RoomAvailabilities { get; set; }
    }
}
