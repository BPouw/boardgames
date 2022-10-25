using System;
namespace Core.DomainServices.Validator
{
    public interface IGameNightValidator
    {
        public bool DateInPresent(DateTime date);

    }
}

