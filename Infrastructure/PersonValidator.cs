using System;
using Core.DomainServices;

namespace Infrastructure
{
    public class PersonValidator : IPersonValidator
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
    }
}

