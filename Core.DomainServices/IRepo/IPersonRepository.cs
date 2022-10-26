using System;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IPersonRepository
    {
        Person GetPersonFromEmail(string email);
        Person GetPersonById(int id);
        Task CreatePerson(Person person);
    }
}

