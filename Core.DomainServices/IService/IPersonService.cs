using System;
using Core.Domain;

namespace Core.DomainServices.IService
{
    public interface IPersonService
    {
        Person getPersonFromEmail(string email);
    }
}

