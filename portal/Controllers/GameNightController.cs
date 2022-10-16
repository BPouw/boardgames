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
    public class GameNightController: Controller
    {
        private readonly ILogger<GameNightController> _logger;
        private IGameNightRepository _gameNightRepository;
        private IGameRepository _gameRepository;
        private IPersonRepository _personRepository;
        private IGameListRepository _gameListRepository;
        private IPersonValidator _personValidator;
        private IGameNightValidator _gameNightValidator;

        private Person person;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public GameNightController(ILogger<GameNightController> logger, IGameNightRepository gameNightRepository, IHttpContextAccessor httpContextAccessor, IGameRepository gameRepository, IPersonRepository personRepository, IGameListRepository gameListRepository,
            IGameNightValidator gameNightValidator, IPersonValidator personValidator) 
        {
            this._logger = logger;
            this._gameNightRepository = gameNightRepository;
            this._httpContextAccessor = httpContextAccessor;
            this._gameRepository = gameRepository;
            this._personRepository = personRepository;
            this._gameListRepository = gameListRepository;
            this._personValidator = personValidator;
            this._gameNightValidator = gameNightValidator;
        }

        public IActionResult Index()
        {
            return View(_gameNightRepository.getGameNights().ToViewModel());
        }

        public IActionResult JoinedGameNights()
        {
            ViewBag.Person = this.person;
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
                    this.person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
                    GameNightToCreate.MaxPlayers = newGameNight.MaxPlayers;
                    GameNightToCreate.Name = newGameNight.Name;
                    GameNightToCreate.DateTime = newGameNight.GameTime;
                    GameNightToCreate.AdultsOnly = newGameNight.AdultsOnly;

                    GameNightToCreate.AddressId = this.person.AddressId;
                    GameNightToCreate.OrganiserId = this.person.Id;

                    await _gameNightRepository.AddGameNight(GameNightToCreate);

                    foreach (int id in newGameNight.GameIds)
                    {
                        GameList gameList = new GameList();
                        gameList.GameId = id;
                        gameList.GameNightId = GameNightToCreate.Id;
                        this._gameListRepository.AddGameToGameNight(gameList);
                    }

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("GameTime", "A game has to be in the future boss");

            }

            PrefillSelectOptions();
            return View(newGameNight);

        }

        private void PrefillSelectOptions()
        {
            var games = _gameRepository.getAllGames();
            ViewBag.Games = new SelectList(games, "Id", "Name");
        }

    }
}

