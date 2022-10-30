using System;
using System.ComponentModel.DataAnnotations;
using Core.Domain;
using Core.DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace portal.Models
{
    public class NewGameNightViewModel 
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime GameTime { get; set; }

        [Required]
        public int MaxPlayers { get; set; }

        [Required]
        public bool AdultsOnly { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public int OrganiserId { get; set; }

        [Required]
        public int[] GameIds { get; set; }

        [Required]
        public Boolean Vegan { get; set; }

        [Required]
        public Boolean LactoseIntolerant { get; set; }

        [Required]
        public Boolean NutAllergy { get; set; }

        [Required]
        public Boolean AlcoholFree { get; set; }
    }
}

