using System;
using Core.DomainServices;
using Core.DomainServices.Service;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Core.DomainServices.IService;
using Core.DomainServices.IValidator;
using Core.DomainServices.Validator;
using Infrastructure;
using Webservice.Dtos;
using System.Net;

namespace Webservice
{
    [ApiController]
    [Route("restapi/gamenights/")]
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

        [HttpGet("{id:int}")]
        public ActionResult<GameNightDto> GetGameNight(int id)
        {
            try
            {
                GameNightDto dto = _gameNightService.GetGameNightById(id).ToDTO();
                return (Ok(dto));
            } catch(DomainException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("{GameNightId:int}/players/{PersonId:int}")]
        public async Task<IActionResult> JoinGameNight(int GameNightId, int PersonId)
        {
            Person Person = _personRepository.GetPersonById(PersonId);
            List<string> Warnings = new List<string>();
            try
            {
                Warnings = await _gameNightService.JoinGameNight(GameNightId, Person);
                return CreatedAtAction("JoinGameNight", Warnings.ToDTO());
            }
            catch (DomainException e)
            {
                return BadRequest(new {error = e.Message});
            }
        }
    }
}



