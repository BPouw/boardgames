using System;
using Core.DomainServices;

namespace Infrastructure
{
    public class GameNightValidator : IGameNightValidator
    {
        public bool DateInPresent(DateTime date)
        {
            if (date > DateTime.Now.Date)
            {
                return true;
            }
            return false;
        }
    }
}

