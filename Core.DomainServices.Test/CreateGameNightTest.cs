using System;
using Core.Domain;
using Core.DomainServices;
using Core.DomainServices.Service;
using Core.DomainServices.Validator;
using Moq;

namespace Core.DomainServices.Test
{
    public class CreateGameNightTest
    {
        [Fact]
        public async Task Person_Under_18_Can_Not_Create_GameNightAsync()
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

            int[] gameIds = new int[] {
                1,2,3
            };

            var Organiser = new Person();
            Organiser.Id = 1;
            Organiser.DateOfBirth = new DateTime(2005, 05, 09);
            DomainException Exception = null;

            //Act
            try
            {
                await _gameNightService.CreateGameNight(GameNight, gameIds, Organiser);
            } catch(DomainException e)
            {
                Exception = e;
            }

            //Assert
            Assert.Equal(Exception.Message, "You must be 18 to host a game night");

        }

        [Fact]
        public async Task Person_Over_18_Can_Create_GameNightAsync()
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
            GameNight.AdultsOnly = false;
            GameNight.DateTime = new DateTime(2023, 10, 10);

            int[] gameIds = new int[] {
                1,2
            };

            gameRepoMock.Setup(gameRepo => gameRepo.GetById(1)).Returns(new Game() { AdultsOnly = false});
            gameRepoMock.Setup(gameRepo => gameRepo.GetById(2)).Returns(new Game() { AdultsOnly = false });

            var Organiser = new Person();
            Organiser.Id = 1;
            Organiser.DateOfBirth = new DateTime(1998, 05, 09);
            DomainException Exception = null;

            //Act
            try
            {
                await _gameNightService.CreateGameNight(GameNight, gameIds, Organiser);
            }
            catch (DomainException e)
            {
                Exception = e;
            }

            //Assert
            Assert.Null(Exception);
        }

        [Fact]
        public async Task Adult_Board_Game_Makes_Game_Night_AdultAsync()
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
            GameNight.AdultsOnly = false;
            GameNight.DateTime = new DateTime(2023, 10, 10);

            int[] gameIds = new int[] {
                1,2
            };

            gameRepoMock.Setup(gameRepo => gameRepo.GetById(1)).Returns(new Game() { AdultsOnly = false, Name = "Kid game" });
            gameRepoMock.Setup(gameRepo => gameRepo.GetById(2)).Returns(new Game() { AdultsOnly = true, Name = "Adult Game"});

            var Organiser = new Person();
            Organiser.Id = 1;
            Organiser.DateOfBirth = new DateTime(1998, 05, 09);

            //Act

            await _gameNightService.CreateGameNight(GameNight, gameIds, Organiser);


            //Assert
            Assert.True(GameNight.AdultsOnly);
        }

        [Fact]
        public async Task Family_Board_Game_Keeps_Game_Night_FamilyAsync()
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
            GameNight.AdultsOnly = false;
            GameNight.DateTime = new DateTime(2023, 10, 10);

            int[] gameIds = new int[] {
                1,2
            };

            gameRepoMock.Setup(gameRepo => gameRepo.GetById(1)).Returns(new Game() { AdultsOnly = false, Name = "Kid game" });
            gameRepoMock.Setup(gameRepo => gameRepo.GetById(2)).Returns(new Game() { AdultsOnly = false, Name = "Kid Game" });

            var Organiser = new Person();
            Organiser.Id = 1;
            Organiser.DateOfBirth = new DateTime(1998, 05, 09);

            //Act

            await _gameNightService.CreateGameNight(GameNight, gameIds, Organiser);


            //Assert
            Assert.False(GameNight.AdultsOnly);
        }
    }
}

