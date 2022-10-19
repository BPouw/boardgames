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
            return _context.Game;
        }

        public IEnumerable<Game> getAllChildFriendlyGames()
        {
            return _context.Game.Where(g => g.AdultsOnly == false);
        }

        public Game GetById(int gameId)
        {
            return _context.Game.SingleOrDefault(g => g.Id == gameId);
        }
    }
}

