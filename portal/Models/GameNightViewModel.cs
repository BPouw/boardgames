using System;
using Core.Domain;
namespace portal.Models
    {
        public class GamenightViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime DateTime { get; set; }
            public int AddressID { get; set; }

            public int MaxPlayers { get; set; }
            public int OrganizerId { get; set; }
            public Person Organizer { get; set; }

        }
    }



