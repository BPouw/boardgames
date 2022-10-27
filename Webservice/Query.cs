using System;
using Core.Domain;
using Core.DomainServices;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Webservice
{
    public class Query
    {
        public IEnumerable<GameNight> GetGameNights(IGameNightRepository gameNightRepository) => gameNightRepository.getGameNights();
    }    
}
