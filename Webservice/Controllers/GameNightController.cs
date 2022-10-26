using System;
using Core.DomainServices;
using Core.DomainServices.Service;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Webservice
{
    [ApiController]
    [Route("restapi/[controller]/")]
    public class GameNightController : ControllerBase
    {
        private GameNightService _gameNightService;
        private IPersonRepository _personRepository;

        public GameNightController(GameNightService gameNightService, IPersonRepository personRepository)
        {
            _gameNightService = gameNightService;
            _personRepository = personRepository;
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
            } catch(DomainException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

