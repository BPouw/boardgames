using System;
using Core.DomainServices.IValidator;

namespace Core.DomainServices.Validator
{
    public class UserValidator: IPersonValidator
    {
        public bool CheckAge(DateTime date)
        {
            DateTime dateToday = DateTime.Today;
            double age = Math.Floor((dateToday.Date - date.Date).Days / 365.25);
            if (age >= 18)
            {
                return true;
            }
            return false;
        }

        public bool CheckAgeMinimumSixteen(DateTime dateOfBirth)
        {
            DateTime date1 = DateTime.Today;
            double age = Math.Floor((date1.Date - dateOfBirth.Date).Days / 365.25);

            if (age >= 16)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

