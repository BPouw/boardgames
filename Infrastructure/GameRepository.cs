using System;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure
{
    public class GameRepository: IGameRepository
    {
        private readonly BoardgamesContext _context;

        public GameRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public IEnumerable<Game> getAllGames()
        {
            return _context.Games;
        }

        public IEnumerable<Game> getAllChildFriendlyGames()
        {
            return _context.Games.Where(g => g.AdultsOnly == false);
        }

        public Game GetById(int gameId)
        {
            return _context.Games.SingleOrDefault(g => g.Id == gameId);
        }
    }
}

