using System;
using Core.Domain;

namespace portal.Models
{

    public class PersonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        public enum Genders { M, V, X };
        public Genders Gender { get; set; }

        public int NoShows { get; set; }
        public int Shows { get; set; }

        public ICollection<GameNight> GameNights { get; set; }

        public ICollection<GameNight> OrganizedGameNights { get; set; }

        public int AddressID { get; set; }
        public Address Address { get; set; }


    }
}

