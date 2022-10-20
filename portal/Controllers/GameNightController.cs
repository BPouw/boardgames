using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Domain;
using Core.DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private string PERSON_SESSION = "PersonObject";

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
            _toastNotification.Success("A success toast that will last for 10 seconds.", 10);
            return View(_gameNightRepository.getGameNights().ToViewModel());
        }

        public IActionResult JoinedGameNights()
        {
            //ViewBag.Gamenights = _playersRepository.getGameNights();
            //ViewBag.Players = _playersRepository.getAllPlayersFromGameNight();
            return View();
        }

        [HttpGet]
        public IActionResult CreateGameNight()
        {
            var model = new NewGameNightViewModel();
            PrefillSelectOptions();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameNight(NewGameNightViewModel newGameNight)
        {
            if (ModelState.IsValid)
            {

                var GameNightToCreate = new GameNight();

                if (_gameNightValidator.DateInPresent(newGameNight.GameTime))
                {
                    Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
                    GameNightToCreate.MaxPlayers = newGameNight.MaxPlayers;
                    GameNightToCreate.Name = newGameNight.Name;
                    GameNightToCreate.DateTime = newGameNight.GameTime;
                    GameNightToCreate.AdultsOnly = newGameNight.AdultsOnly;

                    GameNightToCreate.AddressId = person.AddressId; 
                    GameNightToCreate.OrganiserId = person.Id;

                    // create gamenight
                    await _gameNightRepository.AddGameNight(GameNightToCreate);

                    int[] annoying = newGameNight.GameIds;

                    // add the games to the night
                    //await _gameNightGameRepository.AddManyGamesToGameNight(annoying, GameNightToCreate.Id);

                    GameNightPlayer organiser = new GameNightPlayer();
                    organiser.GameNightId = GameNightToCreate.Id;
                    organiser.PersonId = person.Id;

                    // add the organiser to the gamenight 
                    await _gameNightPlayerRepository.AddPlayer(organiser);

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("GameTime", "A game has to be in the future");

            }

            PrefillSelectOptions();
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

            if (gameNight.AdultsOnly && !_personValidator.CheckAge(person.DateOfBirth)) {
                // gooi een too young error op de UI
                _toastNotification.Warning("You are too young to join this gamenight", 10);
                return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
            } else
            {
                try
                {
                    await this._gameNightPlayerRepository.AddPlayer(player);
                } catch (Exception e)
                {
                    _toastNotification.Warning("You have already joined this gamenight", 10);
                }

                _toastNotification.Success("You joined this gamenight", 10);

                return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
            }
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
            } catch (Exception e)
            {
                _toastNotification.Warning("You are not a participant of this gamenight", 10);
            }

            _toastNotification.Warning("You left this gamenight", 10);

            return RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
        }

        [HttpGet]
        public IActionResult HostedGameNights()
        {
            return View();
        }


        private void PrefillSelectOptions()
        {
            var games = _gameRepository.getAllGames();
            ViewBag.Games = new SelectList(games, "Id", "Name");
        }

    }
}

