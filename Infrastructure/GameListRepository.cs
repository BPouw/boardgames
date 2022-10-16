using System;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure
{
    public class GameListRepository: IGameListRepository
    {
        private readonly BoardgamesContext _context;

        public GameListRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public async Task AddGameToGameNight(GameList gameList)
        {
            _context.GameList.Add(gameList);
            await _context.SaveChangesAsync();
        }
    }
}

