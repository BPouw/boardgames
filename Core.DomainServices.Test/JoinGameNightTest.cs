using System;
using Core.Domain;
using Core.DomainServices;
using Core.DomainServices.Service;
using Core.DomainServices.Validator;
using Moq;

namespace Core.DomainServices.Test
{
    public class JoinGameNightTest
    {

        [Fact]
        public async Task Under_18_Player_Can_Not_Join_Adult_GameNightAsync()
        {
            //Arrange
            var playerRepoMock = new Mock<IPersonRepository>();
            var gameNightRepoMock = new Mock<IGameNightRepository>();
            var gameNightGameRepoMock = new Mock<IGameNightGameRepository>();
            var gameNightPlayerRepoMock = new Mock<IGameNightPlayerRepository>();
            var gameRepoMock = new Mock<IGameRepository>();
            var gameNightValidator = new GameNightValidator();
            var personValidator = new UserValidator();

            GameNightService _gs = new GameNightService(gameNightRepoMock.Object, gameNightGameRepoMock.Object, gameNightPlayerRepoMock.Object, gameNightValidator, gameRepoMock.Object, personValidator);

            var GameNight = new GameNight();
            GameNight.Id = 1;
            GameNight.AdultsOnly = true;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(2005, 05, 09);

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);

            DomainException expectedException = null;


            // Act
            try
            {
                await _gs.JoinGameNight(1, Person);
            } catch (DomainException ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.Equal(expectedException.Message, "You are too young to join this game night");

        }
    }
}

