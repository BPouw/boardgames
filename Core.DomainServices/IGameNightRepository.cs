using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IGameNightRepository
    {
        IQueryable<GameNight> getGameNights();
        IEnumerable<GameNight> getGameNightsByOrganiser(int id);
        Task AddGameNight(GameNight gameNight);
    }
}
