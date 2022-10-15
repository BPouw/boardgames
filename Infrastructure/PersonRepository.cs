using System;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure
{
    public class PersonRepository: IPersonRepository
    {
        private readonly BoardgamesContext _context;

        public PersonRepository(BoardgamesContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetPersonFromEmail(string email)
        {
            var p = _context.People.Where(person => person.Email.Equals(email));
            return p.ToList();
        }
    }
}

