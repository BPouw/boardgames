using System;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IPersonRepository
    {
        Person GetPersonFromEmail(string email);
        Task CreatePerson(Person person);
    }
}

