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
        public Boolean Vegan { get; set; }
        public Boolean LactoseIntolerant { get; set; }
        public Boolean NutAllergy { get; set; }
        public Boolean AlcoholFree { get; set; }
        public ICollection<GameNight> GameNights { get; set; }
        public ICollection<GameNight> OrganisedGameNights { get; set; }
        public ICollection<Review> ReceivedReviews { get; set; }
        public ICollection<Review> WrittenReviews { get; set; }
    }
}
