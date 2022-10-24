using System;
namespace Core.Domain
{
    public class PersonReview
    {
        public Person Person { get; private set; }
        public int PersonId { get; set; }

        public Review Review { get; private set; }
        public int ReviewId { get; set; }
    }
}

