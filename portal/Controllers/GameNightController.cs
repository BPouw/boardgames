using System;
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
        private string PERSON_SESSION = "PersonObject";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public GameNightController(ILogger<GameNightController> logger, IGameNightRepository gameNightRepository, IHttpContextAccessor httpContextAccessor, IGameRepository gameRepository, IPersonRepository personRepository, IGameNightGameRepository gameListRepository,
            IGameNightValidator gameNightValidator, IGameNightPlayerRepository playerRepository, IPersonValidator personValidator)
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
        }

        public IActionResult Index()
        {
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

            if (HttpContext.Session.GetInt32("JoinedStatusId") == id) {
                ViewBag.JoinedStatus = HttpContext.Session.GetString("JoinedStatus");
            } else {
                ViewBag.JoinedStatus = "";
            }

            return View(gameNight.ToViewModel());
        }

        public async Task JoinGameNight()
        {
            GameNightPlayer player = new GameNightPlayer();
            int? id = HttpContext.Session.GetInt32("GameNightId");
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
            GameNight gameNight = _gameNightRepository.getGameNightById((int)id);
            player.PersonId = person.Id;
            player.GameNightId = gameNight.Id;

            if (gameNight.AdultsOnly && !_personValidator.CheckAge(person.DateOfBirth)) {
                HttpContext.Session.SetInt32("JoinedStatusId", (int)id);
                HttpContext.Session.SetString("JoinedStatus", "You are too young to participate in this game night");
                ViewBag.JoinedStatus = "You are too young to participate in this game night";
                // redirect or something ARRHGHGHHGHGHG
            } else
            {
                await this._gameNightPlayerRepository.AddPlayer(player);
                ViewBag.JoinedStatus = "You joined this gamenight!";

                RedirectToAction("DetailsGameNight", new { id = gameNight.Id });
            }


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

