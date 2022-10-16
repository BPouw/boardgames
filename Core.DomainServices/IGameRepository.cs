using System;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IGameRepository
    {
        IEnumerable<Game> getAllGames();
        Game GetById(int gameId);
    }
}

