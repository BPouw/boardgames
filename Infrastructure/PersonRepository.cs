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

        public async Task CreatePerson(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

        }

        public Person GetPersonFromEmail(string email)
        {
            return _context.Person.SingleOrDefault(p => p.Email == email);
        }
    }
}

