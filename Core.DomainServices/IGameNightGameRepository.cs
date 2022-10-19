using System;
using Core.Domain;
namespace Core.DomainServices
{
    public interface IGameNightGameRepository
    {
        Task AddGameToGameNight(GameNightGame gameNightGame);
        Task AddManyGamesToGameNight(int[] GameNightGameId, int GameNightId);
    }
}

