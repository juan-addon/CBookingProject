using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.API.Data.Entities
{
    public class RoomType
    {
        public int Id { get; set; }

        [Display(Name ="Room Type")]
        [MaxLength(50,ErrorMessage ="El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage ="El campo es obligatorio")]
        public string RoomDescription { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Hotel")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [ForeignKey("RoomTypeHotelIdFK")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<RoomAvailability> RoomAvailabilities { get; set; }
    }
}
