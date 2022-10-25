﻿using System;
namespace Core.DomainServices
{
    public interface IPersonValidator
    {
        bool CheckAge(DateTime date);
        bool CheckAgeMinimumSixteen(DateTime dateOfBirth);
    }
}

