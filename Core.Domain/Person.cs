using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public int NoShows { get; set; }
        public int Shows { get; set; }
        public ICollection<GameNight> GameNights { get; set; }
        public ICollection<GameNight> OrganisedGameNights { get; set; }
    }
}
