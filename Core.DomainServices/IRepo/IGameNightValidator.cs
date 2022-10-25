using System;
namespace Core.DomainServices
{
    public interface IGameNightValidator
    {
        bool DateInPresent(DateTime date);
    }
}

