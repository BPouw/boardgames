using System;
using Core.Domain;

namespace Core.DomainServices.IService
{
    public interface IPersonService
    {
        Person getPersonFromEmail(string email);
        bool PersonIs18(Person person);
        bool PersonIs16(DateTime DateOfBirth);
    }
}

