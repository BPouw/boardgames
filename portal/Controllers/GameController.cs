using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DomainServices;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace portal.Controllers
{
    public class GameController : Controller
    {
        private IGameNightGameRepository _gameNightRepository;

        public GameController(IGameNightGameRepository gameNightGameRepository) {
            this._gameNightRepository = gameNightGameRepository;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GameDetails(int id)
        //{
        //    GameNightGame game = _gameNightRepository.GetGameFromId(id);
        //    return View(game.);
        //}
    }
}

