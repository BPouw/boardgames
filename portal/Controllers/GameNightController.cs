using System;
using Core.Domain;
using Core.DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.Models;

namespace portal.Controllers
{
    [Authorize]
    public class GameNightController: Controller
    {
        private readonly ILogger<GameNightController> _logger;
        private IGameNightRepository _gameNightRepository;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private string PERSON_SESSION = "PersonObject";

        public GameNightController(ILogger<GameNightController> logger, IGameNightRepository gameNightRepository, IHttpContextAccessor httpContextAccessor) 
        {
            this._logger = logger;
            this._gameNightRepository = gameNightRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View(_gameNightRepository.getGameNights().ToViewModel());
        }

        public IActionResult JoinedGameNights()
        {
            var personObject = HttpContext.Session.GetObject<Person>(PERSON_SESSION);
            ViewBag.Person = personObject;
            //ViewBag.Gamenights = _playersRepository.getGameNights();
            //ViewBag.Players = _playersRepository.getAllPlayersFromGameNight();

            return View();
        }
    }
}

