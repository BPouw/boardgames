using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DomainServices;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using portal.Models;
using Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace portal.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository _gameRepository;
        public BoardgamesContext BoardgamesContext { get; }

        public GameController(IGameRepository gameRepository, BoardgamesContext boardgamesContext) {
            this._gameRepository = gameRepository;
            BoardgamesContext = boardgamesContext ?? throw new ArgumentNullException(nameof(boardgamesContext));
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GameDetails(int id)
        {
            Game game = _gameRepository.GetById(id);
            int OriginalGameNight = (int)HttpContext.Session.GetInt32("GameNightId");
            ViewBag.GameNightId = OriginalGameNight;
            return View(game.ToViewModel());
        }

        [HttpGet]
        public IActionResult AddGameImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGameImage(GameImageViewModel personImageViewModel)
        {
            if (personImageViewModel is null)
            {
                throw new ArgumentNullException(nameof(personImageViewModel));
            }

            if (!ModelState.IsValid)
            {
                return View();

            }

            var user = new GameImage
            {
                Name = personImageViewModel.Name,

                PictureFormat = personImageViewModel.Picture.ContentType
            };

            var memoryStream = new MemoryStream();
            personImageViewModel.Picture.CopyTo(memoryStream);
            user.Picture = memoryStream.ToArray();

            BoardgamesContext.Add(user);
            BoardgamesContext.SaveChanges();

            return RedirectToAction("Index", "Gamenight");
        }


    }
}

