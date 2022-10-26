using System;
using Core.Domain;
using Core.DomainServices;
using Core.DomainServices.Service;
using Core.DomainServices.Validator;
using Moq;

namespace Core.DomainServices.Test
{
    public class RegisterPersonTest
    {
        [Fact]
        public void Under_16_Player_Can_Not_Register()
        {
            //Arrange
            Person Person = new Person();

            Person.DateOfBirth = new DateTime(2010, 10, 10);

            var PersonRepo = new Mock<IPersonRepository>();
            var PersonValidator = new UserValidator();

            PersonService personService = new PersonService(PersonRepo.Object, PersonValidator);

            //Act
            bool result = personService.PersonIs16(Person.DateOfBirth);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Over_16_Player_Can_Register()
        {
            //Arrange
            Person Person = new Person();

            Person.DateOfBirth = new DateTime(1998, 10, 10);

            var PersonRepo = new Mock<IPersonRepository>();
            var PersonValidator = new UserValidator();

            PersonService personService = new PersonService(PersonRepo.Object, PersonValidator);

            //Act
            bool result = personService.PersonIs16(Person.DateOfBirth);

            //Assert
            Assert.True(result);
        }
    }
}

