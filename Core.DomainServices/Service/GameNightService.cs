using System;
using System.Collections;
using System.Numerics;
using Core.Domain;
using Core.DomainServices.IService;
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

        public GameNightService(IGameNightRepository gameNightRepository, IGameNightGameRepository gameNightGameRepository, IGameNightPlayerRepository gameNightPlayerRepository, IGameNightValidator gameNightValidator,
            IGameRepository gameRepository)
        {
            this._gameNightRepository = gameNightRepository;
            this._gameNightGameRepository = gameNightGameRepository;
            this._gameNightPlayerRepository = gameNightPlayerRepository;
            this._gameNightValidator = gameNightValidator;
            this._gameRepository = gameRepository;
        }

        public List<string> JoinGameNight(int gameNightId, Person person)
        {
            List<string> warnings = new List<string>();
            GameNight gameNight = _gameNightRepository.getGameNightById(gameNightId);

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

            this._gameNightPlayerRepository.AddPlayer(player);

            return warnings;
        }

        public void LeaveGameNight(int gameNightId, Person person)
        {
            GameNightPlayer player = new GameNightPlayer();
            GameNight gameNight = _gameNightRepository.getGameNightById(gameNightId);
            player.PersonId = person.Id;
            player.GameNightId = gameNight.Id;

            try
            {
                this._gameNightPlayerRepository.DeletePlayer(player);           
            } catch
            {
                throw new DomainException("You are not a participant of this gamenight");
            }
        }

        public List<string> CreateGameNight(GameNight gameNight, int[] GameIds, int OrganiserId)
        {
            List<string> warnings = new List<string>();
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
                _gameNightRepository.AddGameNight(gameNight);
                _gameNightGameRepository.AddManyGamesToGameNight(GameIds, gameNight.Id);

                GameNightPlayer organiser = new GameNightPlayer();
                organiser.GameNightId = gameNight.Id;
                organiser.PersonId = OrganiserId;
                _gameNightPlayerRepository.AddPlayer(organiser);

                return warnings;

            } else
            {
                throw new DomainException("Game night must be in the future");
            }
        }

        public List<string> DeleteGameNight(int gameNightId)
        {
            List<string> warnings = new List<string>();
            GameNight gameNight = _gameNightRepository.getGameNightById(gameNightId);

            if (gameNight.Players.Count > 1)
            {
                warnings.Add("A gamenight can't be removed after players have joined");
            }

            _gameNightRepository.DeleteGameNight(gameNight);
            return warnings;
        }

        public List<string> EditGameNight(GameNight gameNight, int[] GameIds)
        {
            List<string> warnings = new List<string>();
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


                _gameNightRepository.UpdateGameNight(gameNight);
                _gameNightGameRepository.UpdateManyGamesToGameNight(GameIds, gameNight.Id);

                return warnings;

            }
            else
            {
                throw new DomainException("A game night has to be in future");
            }

        }
    }
}

