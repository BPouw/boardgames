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

        [Fact]
        public async Task Adult_Player_Can_Join_Adult_GameNightAsync()
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
            GameNight.DateTime = new DateTime(2023, 10, 09);
            GameNight.MaxPlayers = 10;

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(1998, 05, 09);

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.HasJoinedGameNightOnDay(Person, GameNight.DateTime)).Returns(false);

            DomainException Exception = null;


            // Act
            try
            {
                await _gs.JoinGameNight(1, Person);
            }
            catch (DomainException ex)
            {
                Exception = ex;
            }

            // Assert
            Assert.Null(Exception);
        }

        [Fact]
        public async Task Player_Can_Not_Join_When_GameNight_FullAsync()
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
            GameNight.DateTime = new DateTime(2023, 10, 09);
            GameNight.MaxPlayers = 2;

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(1998, 05, 09);

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.HasJoinedGameNightOnDay(Person, GameNight.DateTime)).Returns(false);

            DomainException Exception = null;


            // Act
            try
            {
                await _gs.JoinGameNight(1, Person);
            }
            catch (DomainException ex)
            {
                Exception = ex;
            }

            // Assert
            Assert.Equal(Exception.Message, "This gamenight is full");
        }

        [Fact]
        public async Task Player_Can_Join_When_GameNight_Not_FullAsync()
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
            GameNight.DateTime = new DateTime(2023, 10, 09);
            GameNight.MaxPlayers = 4;

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(1998, 05, 09);

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.HasJoinedGameNightOnDay(Person, GameNight.DateTime)).Returns(false);

            DomainException Exception = null;


            // Act
            try
            {
                await _gs.JoinGameNight(1, Person);
            }
            catch (DomainException ex)
            {
                Exception = ex;
            }

            // Assert
            Assert.Null(Exception);
        }

        [Fact]
        public async Task Player_Can_Not_Join_When_Already_Has_A_GameNightAsync()
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
            GameNight.DateTime = new DateTime(2023, 10, 09);
            GameNight.MaxPlayers = 4;

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(1998, 05, 09);

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.HasJoinedGameNightOnDay(Person, GameNight.DateTime)).Returns(true);

            DomainException Exception = null;


            // Act
            try
            {
                await _gs.JoinGameNight(1, Person);
            }
            catch (DomainException ex)
            {
                Exception = ex;
            }

            // Assert
            Assert.Equal(Exception.Message, "You can not join two game nights on the same day");
        }

        [Fact]
        public async Task Player_Can_Join_When_Does_Not_Has_A_GameNightAsync()
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
            GameNight.DateTime = new DateTime(2023, 10, 09);
            GameNight.MaxPlayers = 4;

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(1998, 05, 09);

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.HasJoinedGameNightOnDay(Person, GameNight.DateTime)).Returns(false);

            DomainException Exception = null;

            // Act
            try
            {
                await _gs.JoinGameNight(1, Person);
            }
            catch (DomainException ex)
            {
                Exception = ex;
            }

            // Assert
            Assert.Null(Exception);
        }

        [Fact]
        public async Task Return_Warning_When_Diet_Does_Not_MatchAsync()
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
            GameNight.DateTime = new DateTime(2023, 10, 09);
            GameNight.MaxPlayers = 4;
            GameNight.NutAllergy = true;

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(1998, 05, 09);
            Person.NutAllergy = true;

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.HasJoinedGameNightOnDay(Person, GameNight.DateTime)).Returns(false);
            List<string> warnings = new List<string>();

            //Act

            warnings = await _gs.JoinGameNight(1, Person);


            // Assert
            Assert.Equal(warnings[0], "This gamenight contains nuts");
        }

        [Fact]
        public async Task Return_Nothing_When_Diet_Does_MatchAsync()
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
            GameNight.DateTime = new DateTime(2023, 10, 09);
            GameNight.MaxPlayers = 4;
            GameNight.NutAllergy = false;

            var Person2 = new Person();
            var Person3 = new Person();
            List<Person> personList = new List<Person>()
            {
                Person2,
                Person3
            };

            GameNight.Players = personList;

            var Person = new Person();
            Person.Id = 1;
            Person.DateOfBirth = new DateTime(1998, 05, 09);
            Person.NutAllergy = true;

            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.getGameNightById(1)).Returns(GameNight);
            gameNightRepoMock.Setup(gameNightRepo => gameNightRepo.HasJoinedGameNightOnDay(Person, GameNight.DateTime)).Returns(false);
            List<string> warnings = new List<string>();

            //Act

            warnings = await _gs.JoinGameNight(1, Person);


            // Assert
            Assert.Equal(warnings.Count, 0);
        }


    }
}

