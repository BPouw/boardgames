using System;
using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GameRepository: IGameRepository
    {
        private readonly BoardgamesContext _context;

        public GameRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public IEnumerable<Game> getAllFamilyGames()
        {
            return _context.Game.Where(g => g.AdultsOnly == false);
        }

        public IEnumerable<Game> getAllGames()
        {
            return _context.Game;
        }

        public Game GetById(int gameId)
        {
            return _context.Game.Where(g => g.Id == gameId).Include(g => g.GameImage).First();
        }

    }
}

