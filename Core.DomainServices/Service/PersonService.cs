using System;
using Core.Domain;
using Core.DomainServices.IService;

namespace Core.DomainServices.Service
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }

        public Person getPersonFromEmail(string email)
        {
            if(this._personRepository.GetPersonFromEmail(email) == null)
            {
                throw new DomainException("No user found");
            } else
            {
                return this._personRepository.GetPersonFromEmail(email);
            }
        }
    }
}

