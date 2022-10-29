using System;
using System.Collections;
using System.Numerics;
using Core.Domain;
using Core.DomainServices.IService;
using Core.DomainServices.IValidator;
using Core.DomainServices.Validator;

namespace Core.DomainServices.Service
{
    public class GameNightService : IGameNightService
    {
        private IGameNightRepository _gameNightRepository;
        private IGameNightGameRepository _gameNightGameRepository;
        private IGameNightPlayerRepository _gameNightPlayerRepository;
        private IGameRepository _gameRepository;
        private IGameNightValidator _gameNightValidator;
        private IPersonValidator _personValidator;

        public GameNightService(IGameNightRepository gameNightRepository, IGameNightGameRepository gameNightGameRepository, IGameNightPlayerRepository gameNightPlayerRepository, IGameNightValidator gameNightValidator,
            IGameRepository gameRepository, IPersonValidator personValidator)
        {
            this._gameNightRepository = gameNightRepository;
            this._gameNightGameRepository = gameNightGameRepository;
            this._gameNightPlayerRepository = gameNightPlayerRepository;
            this._gameNightValidator = gameNightValidator;
            this._gameRepository = gameRepository;
            this._personValidator = personValidator;
        }

        public async Task<List<string>> JoinGameNight(int gameNightId, Person person)
        {
            List<string> warnings = new List<string>();
            GameNight gameNight = _gameNightRepository.getGameNightById(gameNightId);

            if (gameNight == null)
            {
                throw new DomainException("This game night does not exist");
            }

            if (gameNight.AdultsOnly && !_personValidator.CheckAge(person.DateOfBirth))
            {
                throw new DomainException("You are too young to join this game night");
            }

            if (_gameNightRepository.HasJoinedGameNightOnDay(person, gameNight.DateTime))
            {
                throw new DomainException("You can not join two game nights on the same day");
            }

            if (gameNight.MaxPlayers == gameNight.Players.Count())
            {
                throw new DomainException("This gamenight is full");
            }

            if (person.AlcoholFree && gameNight.AlcoholFree)
            {
                warnings.Add("This gamenight will have alcohol");
            }

            if (person.NutAllergy && gameNight.NutAllergy)
            {
                warnings.Add("This gamenight contains nuts");
            }

            if (person.Vegan && gameNight.Vegan)
            {
                warnings.Add("This gamenight will not be vegan");
            }

            if (person.LactoseIntolerant && gameNight.LactoseIntolerant)
            {
                warnings.Add("This gamenight will have lactose");
            }

            GameNightPlayer player = new GameNightPlayer();
            player.PersonId = person.Id;
            player.GameNightId = gameNight.Id;

            await this._gameNightPlayerRepository.AddPlayer(player);

            return warnings;
        }

        public async Task LeaveGameNight(int gameNightId, Person person)
        {
            GameNightPlayer player = new GameNightPlayer();
            GameNight gameNight = _gameNightRepository.getGameNightById(gameNightId);
            player.PersonId = person.Id;
            player.GameNightId = gameNight.Id;

            try
            {
                await this._gameNightPlayerRepository.DeletePlayer(player);           
            } catch
            {
                throw new DomainException("You are not a participant of this gamenight");
            }
        }

        public async Task<List<string>> CreateGameNight(GameNight gameNight, int[] GameIds, Person Organiser)
        {
            List<string> warnings = new List<string>();
            if (_gameNightValidator.DateInPresent(gameNight.DateTime))
            {

                if (!_personValidator.CheckAge(Organiser.DateOfBirth))
                {
                    throw new DomainException("You must be 18 to host a game night");
                }
         
                foreach (int id in GameIds)
                {
                    Game Game = _gameRepository.GetById(id);
                    if (Game.AdultsOnly == true)
                    {
                        if (gameNight.AdultsOnly == false)
                        {
                            warnings.Add($"Your game night has been set to adults only because {Game.Name.ToLower()} is 18+");
                            gameNight.AdultsOnly = true;
                        }
                    }
                }
                await _gameNightRepository.AddGameNight(gameNight);
                await _gameNightGameRepository.AddManyGamesToGameNight(GameIds, gameNight.Id);

                GameNightPlayer organiser = new GameNightPlayer();
                organiser.GameNightId = gameNight.Id;
                organiser.PersonId = Organiser.Id;
                await _gameNightPlayerRepository.AddPlayer(organiser);

                return warnings;

            } else
            {
                throw new DomainException("Game night must be in the future");
            }
        }

        public GameNight GetGameNightById(int gameNightId)
        {

                GameNight GameNight = _gameNightRepository.getGameNightById(gameNightId);
                if (GameNight == null)
                {
                    throw new DomainException("A game night with this ID does not exist");
                }
            return GameNight;

        }

        public async Task DeleteGameNight(int gameNightId)
        {
            GameNight gameNight = _gameNightRepository.getGameNightById(gameNightId);

            if (gameNight.Players.Count > 1)
            {
                throw new DomainException("A game night can not be deleted after players have joined");
            }

            await _gameNightRepository.DeleteGameNight(gameNight);
        }

        public async Task<List<string>> EditGameNight(GameNight gameNight, int[] GameIds)
        {
            List<string> warnings = new List<string>();

            if (gameNight.Players.Count > 1)
            {
                throw new DomainException("A game night can not be edited after players have joined");
            }

            if (_gameNightValidator.DateInPresent(gameNight.DateTime))
            {
                foreach (int id in GameIds)
                {
                    Game Game = _gameRepository.GetById(id);
                    if (Game.AdultsOnly == true)
                    {
                        if (gameNight.AdultsOnly == false)
                        {
                            warnings.Add($"Your game night has been set to adults only because {Game.Name.ToLower()} is 18+");
                            gameNight.AdultsOnly = true;
                        }
                    }
                }
                GameNightPlayer gameNightPlayer = new GameNightPlayer
                {
                    GameNightId = gameNight.Id,
                    PersonId = gameNight.OrganiserId
                };

                await _gameNightPlayerRepository.DeletePlayer(gameNightPlayer);

                await _gameNightRepository.UpdateGameNight(gameNight);

                await _gameNightPlayerRepository.AddPlayer(gameNightPlayer);

                await _gameNightGameRepository.UpdateManyGamesToGameNight(GameIds, gameNight.Id);

                return warnings;

            }
            else
            {
                throw new DomainException("A game night has to be in future");
            }

        } 
    }
}

