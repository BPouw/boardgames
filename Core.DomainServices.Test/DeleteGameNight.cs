using System;
using Core.Domain;
using Core.DomainServices.Service;
using Core.DomainServices.Validator;
using Moq;

namespace Core.DomainServices.Test
{
    public class DeleteGameNight
    {
        [Fact]
        public async Task Can_Not_Delete_GameNight_When_Player_Has_Joined()
        {
            //Arrange
            var playerRepoMock = new Mock<IPersonRepository>();
            var gameNightRepoMock = new Mock<IGameNightRepository>();
            var gameNightGameRepoMock = new Mock<IGameNightGameRepository>();
            var gameNightPlayerRepoMock = new Mock<IGameNightPlayerRepository>();
            var gameRepoMock = new Mock<IGameRepository>();
            var gameNightValidator = new GameNightValidator();
            var personValidator = new UserValidator();

            GameNightService _gameNightService = new GameNightService(gameNightRepoMock.Object, gameNightGameRepoMock.Object, gameNightPlayerRepoMock.Object, gameNightValidator, gameRepoMock.Object, personValidator);

            var GameNight = new GameNight();
            GameNight.Id = 1;
            GameNight.AdultsOnly = true;
            GameNight.DateTime = new DateTime(2023, 10, 10);

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Player = new Person()
            {
                Id = 1,
            };

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            DomainException exception = null;

            //Act
            try
            {
                await _gameNightService.DeleteGameNight(GameNight.Id);
            }
            catch (DomainException e)
            {
                exception = e;
            }

            //Assert
            Assert.Equal(exception.Message, "A game night can not be deleted after players have joined");
        }

        [Fact]
        public async Task Can_Delete_GameNight_When_No_Player_Has_Joined()
        {
            //Arrange
            var playerRepoMock = new Mock<IPersonRepository>();
            var gameNightRepoMock = new Mock<IGameNightRepository>();
            var gameNightGameRepoMock = new Mock<IGameNightGameRepository>();
            var gameNightPlayerRepoMock = new Mock<IGameNightPlayerRepository>();
            var gameRepoMock = new Mock<IGameRepository>();
            var gameNightValidator = new GameNightValidator();
            var personValidator = new UserValidator();

            GameNightService _gameNightService = new GameNightService(gameNightRepoMock.Object, gameNightGameRepoMock.Object, gameNightPlayerRepoMock.Object, gameNightValidator, gameRepoMock.Object, personValidator);

            var GameNight = new GameNight();
            GameNight.Id = 1;
            GameNight.AdultsOnly = true;
            GameNight.DateTime = new DateTime(2023, 10, 10);

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>();


            GameNight.Players = personList;

            var Player = new Person()
            {
                Id = 1,
            };

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            DomainException exception = null;

            //Act
            try
            {
                await _gameNightService.DeleteGameNight(GameNight.Id);
            }
            catch (DomainException e)
            {
                exception = e;
            }

            //Assert
            Assert.Null(exception);
        }
    }
}

