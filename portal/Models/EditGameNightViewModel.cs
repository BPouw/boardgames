using System;
using System.ComponentModel.DataAnnotations;

namespace portal.Models
{
    public class EditGameNightViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime GameTime { get; set; }

        public int MaxPlayers { get; set; }

        public bool AdultsOnly { get; set; }

        public int[] GameIds { get; set; }
        public Boolean Vegan { get; set; }
        public Boolean LactoseIntolerant { get; set; }
        public Boolean NutAllergy { get; set; }
        public Boolean AlcoholFree { get; set; }
    }
}

