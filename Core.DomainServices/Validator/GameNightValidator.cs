using System;
namespace Core.DomainServices.Validator
{
    public class GameNightValidator: IGameNightValidator
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

