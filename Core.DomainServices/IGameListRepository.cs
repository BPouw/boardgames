using System;
using Core.Domain;
namespace Core.DomainServices
{
    public interface IGameListRepository
    {
        Task AddGameToGameNight(GameList gameList);
    }
}

