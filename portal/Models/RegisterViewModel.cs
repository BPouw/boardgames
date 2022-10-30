using System;
using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace portal.Models
{
    public class RegisterViewModel
    {
       [Required]
       [EmailAddress(ErrorMessage = "Please enter a valid email")]
       public string Email { get; set; }

        [Required]
        [UIHint("password")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must have 8 characters, one uppercase, one number and one special character")]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public bool Vegan { get; set; }

        [Required]
        public bool LactoseIntolerant { get; set; }

        [Required]
        public bool NutAllergy { get; set; }

        [Required]
        public bool AlcoholFree { get; set; }

    }
}

