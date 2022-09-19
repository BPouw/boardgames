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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public enum Genders { M, V, X };
        public Genders Gender { get; set; }
        public Address Address { get; set; }

        public int NoShows { get; set; }
        public int Shows { get; set; }
    }
}
