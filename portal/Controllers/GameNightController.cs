using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Domain;
using Core.DomainServices;
using Core.DomainServices.IService;
using Core.DomainServices.Service;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Differencing;
using portal.Models;

namespace portal.Controllers
{
    [Authorize]
    public class GameNightController : Controller
    {
        private readonly ILogger<GameNightController> _logger;

        // ew ew repo's
        private IGameNightRepository _gameNightRepository;
        private IGameRepository _gameRepository;
        private IPersonRepository _personRepository;
        private IGameNightGameRepository _gameNightGameRepository;
        private IPersonValidator _personValidator;
        private IGameNightPlayerRepository _gameNightPlayerRepository;
        private IPersonReviewRepository _personReviewRepository;
        private readonly INotyfService _toastNotification;

        // wow wow services
        private IPersonService _personService;
        private IGameNightService _gameNightService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public GameNightController(ILogger<GameNightController> logger, IGameNightRepository gameNightRepository, IHttpContextAccessor httpContextAccessor, IGameRepository gameRepository, IPersonRepository personRepository, IGameNightGameRepository gameListRepository,
             IGameNightPlayerRepository playerRepository, IPersonValidator personValidator, IPersonReviewRepository personReviewRepository, INotyfService toastNotification, IPersonService personService, IGameNightService gameNightService)
        {
            this._logger = logger;
            this._gameNightRepository = gameNightRepository;
            this._httpContextAccessor = httpContextAccessor;
            this._gameRepository = gameRepository;
            this._personRepository = personRepository;
            this._gameNightGameRepository = gameListRepository;
            this._personValidator = personValidator;
            this._gameNightPlayerRepository = playerRepository;
            this._personReviewRepository = personReviewRepository;
            this._toastNotification = toastNotification;

            this._personService = personService;
            this._gameNightService = gameNightService;
        }

        public IActionResult Index()
        {
            try
            {
                Person person = this._personService.getPersonFromEmail(HttpContext.User.Identity.Name);
                
            HttpContext.Session.SetInt32("LoggedInPersonId", person.Id);
            return View(_gameNightRepository.getGameNights().ToViewModel());
            } catch (DomainException d)
            {
                _toastNotification.Error(d.Message, 10);
                return RedirectToAction("Login", "Account");
            } 
        }

        public IActionResult JoinedGameNights()
        {
            try
            {
                Person person = this._personService.getPersonFromEmail(HttpContext.User.Identity.Name);

                HttpContext.Session.SetInt32("LoggedInPersonId", person.Id);
                return View(_gameNightRepository.getJoinedGameNights(person).ToViewModel());
            }
            catch (DomainException d)
            {
                _toastNotification.Error(d.Message, 10);
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult HostedGameNights()
        {
            Person person = this._personService.getPersonFromEmail(HttpContext.User.Identity.Name);
            bool is18 = _personValidator.CheckAge(person.DateOfBirth);
            ViewBag.PersonAge = is18;
            return View(_gameNightRepository.getGameNightsByOrganiser(person.Id).ToViewModel());
        }

        [HttpGet]
        public IActionResult CreateGameNight()
        {
            var model = new NewGameNightViewModel();
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
            PrefillSelectOptions();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameNight(NewGameNightViewModel newGameNight)
        {
            if (ModelState.IsValid)
            {
                var GameNightToCreate = new GameNight();

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

                try
                {
                    List<string> warnings = _gameNightService.CreateGameNight(GameNightToCreate, newGameNight.GameIds, person.Id);
                    foreach (string s in warnings)
                    {
                        _toastNotification.Warning(s, 10);
                    }

                    _toastNotification.Success("You have created a new game night!");

                    return RedirectToAction("Index");

                } catch(DomainException e)
                {
                    _toastNotification.Error(e.Message, 10);
                    PrefillSelectOptions();
                    return View(newGameNight);
                }

               
             }

            PrefillSelectOptions();
            return View(newGameNight);

        }


        [HttpGet]
        public IActionResult DetailsGameNight(int id)
        {
            GameNight gameNight = _gameNightRepository.getGameNightPopulated(id);
            Person person = _personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);
            try
            {
                double doublescore = _personReviewRepository.AverageScoreForPerson(gameNight.OrganiserId);
                ViewBag.AverageScore = Convert.ToInt32(doublescore);
            } catch(Exception e)
            {
                ViewBag.AverageScore = 0;
            }


            HttpContext.Session.SetInt32("GameNightId", id);
            HttpContext.Session.SetInt32("ReviewerId", person.Id);
            HttpContext.Session.SetInt32("OrganiserId", gameNight.OrganiserId);


            ViewBag.PersonId = person.Id;


            return View(gameNight.ToViewModel());
        }

        public async Task<IActionResult> JoinGameNight()
        {
            GameNightPlayer player = new GameNightPlayer();
            int? id = HttpContext.Session.GetInt32("GameNightId");
            try
            {
                Person person = this._personService.getPersonFromEmail(HttpContext.User.Identity.Name);
                try
                {
                    List<string> warnings = _gameNightService.JoinGameNight((int)id, person);

                    foreach (string s in warnings)
                    {
                        _toastNotification.Warning(s, 10);
                    }

                    _toastNotification.Success("You have joined this gamenight");
                    return RedirectToAction("DetailsGameNight", new { id = id });
                }
                catch (DomainException e)
                {
                    _toastNotification.Error(e.Message, 10);
                    return RedirectToAction("DetailsGameNight", new { id = id });
                }
            }
            catch (DomainException d)
            {
                _toastNotification.Error(d.Message, 10);
                return RedirectToAction("Login", "Account");
            }

        }

        public async Task<IActionResult> LeaveGameNight()
        {
            int? id = HttpContext.Session.GetInt32("GameNightId");
            Person person = this._personRepository.GetPersonFromEmail(HttpContext.User.Identity.Name);

            try
            {
                _gameNightService.LeaveGameNight((int)id, person);
                _toastNotification.Success("You have left this game night", 10);
                return RedirectToAction("DetailsGameNight", new { id = id });

            } catch(DomainException e)
            {
                _toastNotification.Error(e.Message, 10);
            }
            return RedirectToAction("DetailsGameNight", new { id = id });

        }

        public async Task<IActionResult> DeleteGameNight()
        {
            int? id = HttpContext.Session.GetInt32("GameNightId");

            List<string> warnings = _gameNightService.DeleteGameNight((int)id);

            foreach(string warning in warnings)
            {
                _toastNotification.Warning(warning, 10);
            }

            _toastNotification.Success("The game night has been removed", 10);
            return RedirectToAction("Index");
        }


        public IActionResult EditGameNight(int id)
        {
            GameNight gameNight = _gameNightRepository.getGameNightPopulated(id);
            EditGameNightViewModel edit = new EditGameNightViewModel();

            edit.Id = gameNight.Id;
            edit.Name = gameNight.Name;
            edit.MaxPlayers = gameNight.MaxPlayers;
            edit.AdultsOnly = gameNight.AdultsOnly;
            edit.AlcoholFree = gameNight.AlcoholFree;
            edit.LactoseIntolerant = gameNight.LactoseIntolerant;
            edit.NutAllergy = gameNight.NutAllergy;
            edit.Vegan = gameNight.Vegan;
            edit.GameTime = gameNight.DateTime;

            PrefillSelectOptions();

            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> EditGameNight(EditGameNightViewModel updatedViewModel)
        {

            if (ModelState.IsValid)
            {
                GameNight originalNight = _gameNightRepository.getGameNightPopulated(updatedViewModel.Id);
                GameNight updatedGameNight = new GameNight();
                updatedGameNight.Id = updatedViewModel.Id;
                updatedGameNight.Name = updatedViewModel.Name;
                updatedGameNight.MaxPlayers = updatedViewModel.MaxPlayers;
                updatedGameNight.AdultsOnly = updatedViewModel.AdultsOnly;
                updatedGameNight.AlcoholFree = updatedViewModel.AlcoholFree;
                updatedGameNight.LactoseIntolerant = updatedViewModel.LactoseIntolerant;
                updatedGameNight.NutAllergy = updatedViewModel.NutAllergy;
                updatedGameNight.Vegan = updatedViewModel.Vegan;
                updatedGameNight.DateTime = updatedViewModel.GameTime;
                updatedGameNight.AddressId = originalNight.AddressId;
                updatedGameNight.OrganiserId = originalNight.OrganiserId;

                try
                {
                    _gameNightService.EditGameNight(updatedGameNight, updatedViewModel.GameIds);
                } catch(DomainException e)
                {
                    _toastNotification.Error(e.Message, 10);
                }

                return RedirectToAction("DetailsGameNight", new { id = updatedGameNight.Id });

            }
            PrefillSelectOptions();
            return View();
        }

        private void PrefillSelectOptions()
        {
            var games = _gameRepository.getAllGames();
            ViewBag.Games = new SelectList(games, "Id", "Name");
        }

    }
}

