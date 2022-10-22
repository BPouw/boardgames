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
        IEnumerable<GameNight> getGameNights();
        IEnumerable<GameNight> getGameNightsByOrganiser(int id);
        IEnumerable<GameNight> getJoinedGameNights(Person person);
        GameNight getGameNightById(int id);
        GameNight getGameNightPopulated(int id);
        Task AddGameNight(GameNight gameNight);
        Task DeleteGameNight(GameNight gameNight);
        Task UpdateGameNight(GameNight gameNight);
    }
}
