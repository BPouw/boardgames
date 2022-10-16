﻿using System;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure
{
    public class PersonRepository: IPersonRepository
    {
        private readonly BoardgamesContext _context;

        public PersonRepository(BoardgamesContext context)
        {
            _context = context;
        }

        public Person GetPersonFromEmail(string email)
        {
            return _context.People.SingleOrDefault(p => p.Email == email);
        }
    }
}

