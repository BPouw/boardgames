using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class GameNight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int OrganiserId { get; set; }
        public Person Organiser { get; set; }
        public int MaxPlayers { get; set; }
        public bool AdultsOnly { get; set; }
        public ICollection<Person> Players { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
