using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    internal class GameNightRepository : IGameNightRepository
    {
        public Task AddGameNight(GameNight gameNight)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameNight> getGameNights()
        {
            throw new NotImplementedException();
        }
    }
}
