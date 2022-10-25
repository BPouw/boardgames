using System;
using Core.Domain;

namespace Core.DomainServices.IService
{
    public interface IGameNightService
    {
        List<string> JoinGameNight(int gameNightId, Person person);
        void LeaveGameNight(int gameNightId, Person person);
        List<string> CreateGameNight(GameNight gameNight, int[] GameIds, int OrganiserId);
        List<string> DeleteGameNight(int gameNightId);
        List<string> EditGameNight(GameNight gameNight, int[] GameIds);
    }
}

