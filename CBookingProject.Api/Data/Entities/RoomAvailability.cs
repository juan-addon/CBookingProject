using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBookingProject.API.Data.Entities
{
    public class RoomAvailability
    {
        [Key]
        public int AvailabilityId { get; set; }

        [Display(Name = "Season")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string AvailabilityDescription { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DateFrom(ErrorMessage = "Back date entry not allowed")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Date To")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; } = true;
        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [ForeignKey("RoomTypeAvailabilitydFK")]
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<RoomPrice> RoomPrices { get; set; }
    }

    /*public class DateFromAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime _dateJoin = Convert.ToDateTime(value);
            if (_dateJoin >= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }*/


}
