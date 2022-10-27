using System;
using Core.DomainServices;
using Core.DomainServices.Service;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Core.DomainServices.IService;
using Core.DomainServices.IValidator;
using Core.DomainServices.Validator;
using Infrastructure;

namespace Webservice
{
    [ApiController]
    [Route("restapi/[controller]/")]
    public class GameNightController : ControllerBase
    {
        private IGameNightService _gameNightService;
        private IPersonRepository _personRepository;
        private IGameNightRepository _gameNightRepository;

        public GameNightController(IGameNightService gameNightService, IPersonRepository personRepository, IGameNightRepository gameNightRepository)
        {
            _gameNightService = gameNightService;
            _personRepository = personRepository;
            _gameNightRepository = gameNightRepository;
        }

        [HttpGet("GetGameNightByOrganiser")]
        public ActionResult<List<GameNight>> GetGameNightsPerOrganizer(int PersonId)
        {
            return Ok(_gameNightRepository.getGameNightsByOrganiser(PersonId));
        }

        [HttpPost("JoinGameNight")]
        public async Task<IActionResult> JoinGameNight(int GameNightId, int PersonId)
        {
            Person Person = _personRepository.GetPersonById(PersonId);
            List<string> Warnings = new List<string>();
            try
            {
                Warnings = await _gameNightService.JoinGameNight(GameNightId, Person);
                return Ok(Warnings);
            }
            catch (DomainException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}



