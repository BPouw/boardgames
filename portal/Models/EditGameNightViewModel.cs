using System;
using System.ComponentModel.DataAnnotations;

namespace portal.Models
{
    public class EditGameNightViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime GameTime { get; set; }
        [Required]
        public int MaxPlayers { get; set; }
        [Required]
        public bool AdultsOnly { get; set; }
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

