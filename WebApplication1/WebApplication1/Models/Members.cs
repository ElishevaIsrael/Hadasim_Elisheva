using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Members
    {
        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "ID number must be 9 digits long.")]
        public string IdNumber { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "City can only contain letters and spaces.")]
        public string City { get; set; }  // City in Address
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Street can only contain letters and spaces.")]
        public string Street { get; set; }  // Street in Address
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Number can only contain digits.")]
        public string Number { get; set; }   // Number in Address
        [Required]
        [DateRange(MinimumAge = 0, ErrorMessage = "Date of Birth must be in the past.")]
        public DateTime DateOfBirth { get; set; }
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be 9 digits long.")]
        public string Phone { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile phone number must be 10 digits long.")]
        public string MobilePhone { get; set; }
    }

    public class Vaccination
    {
        public string IdNumber { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "CoronaVaccine must be between 1 and 4.")]
        public int CoronaVaccine { get; set; }
        public DateTime VaccineDate { get; set; }
        public string VaccineManufacturer { get; set; }
    }

    public class CovidStatus
    {
        public string IdNumber { get; set; }
        public DateTime PositiveTestDate { get; set; }
        public DateTime RecoveryDate { get; set; }
    }
    public class DateRangeAttribute : ValidationAttribute
    {
        public int MinimumAge { get; set; }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            return dt < DateTime.Now && dt.AddYears(MinimumAge) <= DateTime.Now;
        }
    }
}
