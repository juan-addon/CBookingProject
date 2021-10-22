using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBookingProject.API.Data.Entities
{
    public class RoomAvailability
    {
        [Key]
        public int AvailabilityId { get; set; }
        [Display(Name = "Date From")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateFrom(ErrorMessage = "Back date entry not allowed")]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "Date To")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DateTo(DateStartProperty = "StartDate")]
        public DateTime DateTo { get; set; }
        [DefaultValue(true)]
        public bool Status { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
    }

    public class DateFromAttribute : ValidationAttribute
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
    }
}
