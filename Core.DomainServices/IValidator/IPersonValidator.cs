using System;
namespace Core.DomainServices.IValidator
{
    public interface IPersonValidator
    {
        public bool CheckAge(DateTime date);
        public bool CheckAgeMinimumSixteen(DateTime dateOfBirth);
    }
}

