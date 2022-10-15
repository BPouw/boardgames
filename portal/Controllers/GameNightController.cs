using System;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using portal.Models;

namespace portal.Controllers
{
    public class GameNightController: Controller
    {
        private readonly ILogger<GameNightController> _logger;
        private IGameNightRepository _gameNightRepository;

        public GameNightController(ILogger<GameNightController> logger, IGameNightRepository gameNightRepository) 
        {
            this._logger = logger;
            this._gameNightRepository = gameNightRepository;

        }

        public IActionResult Index()
        {
            return View(_gameNightRepository.getGameNights().ToViewModel());
        }
    }
}

