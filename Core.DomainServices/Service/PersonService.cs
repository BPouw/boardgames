using System;
using Core.Domain;
using Core.DomainServices.IService;
using Core.DomainServices.IValidator;

namespace Core.DomainServices.Service
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;
        private IPersonValidator _personValidator;

        public PersonService(IPersonRepository personRepository, IPersonValidator validator)
        {
            this._personRepository = personRepository;
            this._personValidator = validator;
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

        public bool PersonIs18(Person person)
        {
            return _personValidator.CheckAge(person.DateOfBirth);
        }

        public bool PersonIs16(DateTime DateOfBirth)
        {
            return _personValidator.CheckAgeMinimumSixteen(DateOfBirth);
        }


    }
}

