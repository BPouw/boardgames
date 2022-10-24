using System;
namespace Core.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public Person Reviewer { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public ICollection<Person> People { get; set; }
    }
}

