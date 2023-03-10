using System;
using Core.Domain;

namespace Core.DomainServices.IService
{
    public interface IGameNightService
    {
        Task<List<string>> JoinGameNight(int gameNightId, Person person);
        Task LeaveGameNight(int gameNightId, Person person);
        Task<List<string>> CreateGameNight(GameNight gameNight, int[] GameIds, Person OrganiserId);
        Task DeleteGameNight(int gameNightId);
        Task<List<string>> EditGameNight(GameNight gameNight, int[] GameIds);
        GameNight GetGameNightById(int gameNightId);
    }
}

