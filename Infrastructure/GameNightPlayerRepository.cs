using System;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure
{
    public class GameNightPlayerRepository: IGameNightPlayerRepository
    {
        private readonly BoardgamesContext _context;

        public GameNightPlayerRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public async Task AddPlayer(GameNightPlayer player)
        {
            _context.GameNight_Player.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayer(GameNightPlayer player)
        {
            _context.ChangeTracker.Clear();
            _context.GameNight_Player.Remove(player);
            await _context.SaveChangesAsync();
        }

        public GameNightPlayer getGameNightPlayer(GameNightPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}

