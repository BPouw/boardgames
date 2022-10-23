using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Domain;
using Core.DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using portal.Models;

namespace portal.Controllers
{
    [Authorize]
    public class GameNightController : Controller
    {
        private readonly ILogger<GameNightController> _logger;
        private IGameNightRepository _gameNightRepository;
        private IGameRepository _gameRepository;
        private IPersonRepository _personRepository;
        private IGameNightGameRepository _gameNightGameRepository;
        private IPersonValidator _personValidator;
        private IGameNightValidator _gameNightValidator;
        private IGameNightPlayerRepository _gameNightPlayerRepository;
        private readonly INotyfService _toastNotification;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public GameNightController(ILogger<GameNightController> logger, IGameNightRepository gameNightRepository, IHttpContextAccessor httpContextAccessor, IGameRepository gameRepository, IPersonRepository personRepository, IGameNightGameRepository gameListRepository,
            IGameNightValidator gameNightValidator, IGameNightPlayerRepository playerRepository, IPersonValidator personValidator, INotyfService toastNotification)
        {
            this._logger = logger;
            this._gameNightRepository = gameNightRepository;
            this._httpContextAccessor = httpContextAccessor;
            this._gameRepository = gameRepository;
            this._personRepository = personRepository;
            this._gameNightGameRepository = gameListRepository;
            this._personValidator = personValidator;
            this._gameNightValidator = gameNightValidator;
            this._gameNightPlayerRepository = playerRepository;
            this._toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View(_gameNightRepository.getGameNights().ToViewModel());
        }

        public IActionResult JoinedGameNights()
        {
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
            return View(_gameNightRepository.getJoinedGameNights(person).ToViewModel());
        }

        public IActionResult HostedGameNights()
        {
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
            return View(_gameNightRepository.getGameNightsByOrganiser(person.Id).ToViewModel());
        }

        [HttpGet]
        public IActionResult CreateGameNight()
        {
            var model = new NewGameNightViewModel();
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);

            // don't show 18+ games when user is underage
            if(_personValidator.CheckAge(person.DateOfBirth))
            {
                PrefillSelectOptions();
                HttpContext.Session.SetString("Age", "Adult");
                ViewBag.Age = "Adult";
            } else
            {
                PrefillSelectOptionsFamily();
                HttpContext.Session.SetString("Age", "Kid");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameNight(NewGameNightViewModel newGameNight)
        {
            if (ModelState.IsValid)
            {

                var GameNightToCreate = new GameNight();

                // validate that the event is not in the past
                if (_gameNightValidator.DateInPresent(newGameNight.GameTime))
                {
                    Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
                    GameNightToCreate.MaxPlayers = newGameNight.MaxPlayers;
                    GameNightToCreate.Name = newGameNight.Name;
                    GameNightToCreate.DateTime = newGameNight.GameTime;
                    GameNightToCreate.AdultsOnly = newGameNight.AdultsOnly;
                    GameNightToCreate.Vegan = newGameNight.Vegan;
                    GameNightToCreate.LactoseIntolerant = newGameNight.LactoseIntolerant;
                    GameNightToCreate.NutAllergy = newGameNight.NutAllergy;
                    GameNightToCreate.AlcoholFree = newGameNight.AlcoholFree;
                    GameNightToCreate.AddressId = person.AddressId; 
                    GameNightToCreate.OrganiserId = person.Id;

                    // check for 18+ games
                    foreach(int id in newGameNight.GameIds)
                    {
                       Game Game = _gameRepository.GetById(id);
                       if (Game.AdultsOnly == true)
                        {
                            if (GameNightToCreate.AdultsOnly == false)
                            {
                                _toastNotification.Warning($"Your game night has been set to adults only because {Game.Name.ToLower()} is 18+", 10);
                                GameNightToCreate.AdultsOnly = true;
                            }
                        } 
                    }

                    // create gamenight
                    await _gameNightRepository.AddGameNight(GameNightToCreate);

                    // add the games to the gamenight
                    await _gameNightGameRepository.AddManyGamesToGameNight(newGameNight.GameIds, GameNightToCreate.Id);

                    // add the organiser to the gamenight 
                    GameNightPlayer organiser = new GameNightPlayer();
                    organiser.GameNightId = GameNightToCreate.Id;
                    organiser.PersonId = person.Id;
                    await _gameNightPlayerRepository.AddPlayer(organiser);

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("GameTime", "A game has to be in the future");

            }
            string Age = HttpContext.Session.GetString("Age");

            if (Age == "Adult")
            {
                PrefillSelectOptions();
            } else
            {
                PrefillSelectOptionsFamily();
            }

            return View(newGameNight);

        }

        [HttpGet]
        public IActionResult DetailsGameNight(int id)
        {
            GameNight gameNight = _gameNightRepository.getGameNightPopulated(id);
            HttpContext.Session.SetInt32("GameNightId", id);

            Person person = _personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);

            ViewBag.PersonId = person.Id;

            return View(gameNight.ToViewModel());
        }

        public async Task<IActionResult> JoinGameNight()
        {
            GameNightPlayer player = new GameNightPlayer();
            int? id = HttpContext.Session.GetInt32("GameNightId");
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
            GameNight gameNight = _gameNightRepository.getGameNightById((int)id);
            player.PersonId = person.Id;
            player.GameNightId = gameNight.Id;

            // leeftijd validatie
            if (gameNight.AdultsOnly && !_personValidator.CheckAge(person.DateOfBirth)) {
                _toastNotification.Warning("You are too young to join this gamenight", 10);
                return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
            }

            if (_gameNightRepository.HasJoinedGameNightOnDay(person, gameNight.DateTime))
            {
                _toastNotification.Warning("You can not join two game nights on the same day", 10);
                return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
            }

            if (gameNight.MaxPlayers == gameNight.Players.Count())
            {
                _toastNotification.Warning("This gamenight is full", 10);
                return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
            }
            
            if (person.AlcoholFree && gameNight.AlcoholFree)
            {
                _toastNotification.Warning("This gamenight will have alcohol", 10);
            }

            if (person.NutAllergy && gameNight.NutAllergy)
            {
                _toastNotification.Warning("This gamenight contains nuts", 10);
            }

            if (person.Vegan && gameNight.Vegan)
            {
                _toastNotification.Warning("This gamenight will not be vegan", 10);
            }

            if (person.LactoseIntolerant && gameNight.LactoseIntolerant)
            {
                _toastNotification.Warning("This gamenight will have lactose", 10);
            }

            try
            {
               await this._gameNightPlayerRepository.AddPlayer(player);
                _toastNotification.Success("You joined this gamenight", 10);
            } catch (Exception e)
            {
               _toastNotification.Error("You have already joined this gamenight", 10);
            }

            return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });

        }

        public async Task<IActionResult> LeaveGameNight()
        {
            GameNightPlayer player = new GameNightPlayer();
            int? id = HttpContext.Session.GetInt32("GameNightId");
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
            GameNight gameNight = _gameNightRepository.getGameNightById((int)id);
            player.PersonId = person.Id;
            player.GameNightId = gameNight.Id;

            try
            {
                await this._gameNightPlayerRepository.DeletePlayer(player);
                _toastNotification.Warning("You left this gamenight", 10);
        } catch (Exception e)
            {
                _toastNotification.Error("You are not a participant of this gamenight", 10);
            }

            return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
        }

        public async Task<IActionResult> DeleteGameNight()
        {
            int? id = HttpContext.Session.GetInt32("GameNightId");
            GameNight gameNight = _gameNightRepository.getGameNightById((int)id);

            if (gameNight.Players.Count > 1)
            {
                _toastNotification.Error("A game night can not be removed after players have joined", 10);
                return RedirectToAction("Index");
            }

            await _gameNightRepository.DeleteGameNight(gameNight);

            _toastNotification.Warning($"Game night {gameNight.Name.ToLower()} has been removed", 10);
            return RedirectToAction("Index");
        }


        private void PrefillSelectOptions()
        {
            var games = _gameRepository.getAllGames();
            ViewBag.Games = new SelectList(games, "Id", "Name");
        }

        private void PrefillSelectOptionsFamily()
        {
            var games = _gameRepository.getAllFamilyGames();
            ViewBag.Games = new SelectList(games, "Id", "Name");
        }

    }
}

