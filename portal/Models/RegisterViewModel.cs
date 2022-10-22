using System;
using System.ComponentModel.DataAnnotations;
using Core.Domain;

namespace portal.Models
{
    public class RegisterViewModel
    {
       [Required]
       public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

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

