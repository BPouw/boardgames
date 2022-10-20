using System;
using Core.Domain;
namespace portal.Models
    {
        public class GamenightViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime DateTime { get; set; }
            public int AddressId { get; set; }
            public Address Address { get; set; }
            public bool AdultsOnly { get; set; }
            public int MaxPlayers { get; set; } 
            public int OrganiserId { get; set; }
            public Person Organiser { get; set; }

            public Boolean Vegan { get; set; }
              public Boolean LactoseIntolerant { get; set; }
            public Boolean NutAllergy { get; set; }
           public Boolean AlcoholFree { get; set; }
        public ICollection<Person> Players { get; set; }
            public ICollection<Game> Games { get; set; }


    }
    }



