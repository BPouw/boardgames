using System;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersonFromEmail(string email);
    }
}

