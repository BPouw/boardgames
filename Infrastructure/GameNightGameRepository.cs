using System;
using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GameNightGameRepository : IGameNightGameRepository
    {
        private readonly BoardgamesContext _context;

        public GameNightGameRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public async Task AddGameToGameNight(GameNightGame gameNightGame)
        {
            _context.GameNight_Game.Add(gameNightGame);
            await _context.SaveChangesAsync();
        }

        public async Task AddManyGamesToGameNight(int[] GameNightGameId, int GameNightId)
        {
            foreach(int id in GameNightGameId)
            {
                GameNightGame gameNightGame = new GameNightGame();
                gameNightGame.GameNightId = GameNightId;
                gameNightGame.GameId = id;
                _context.GameNight_Game.Add(gameNightGame);
            }
            await _context.SaveChangesAsync();
        }

        public Task GetGameNightPlayer(GameNightGame gameNightGame)
        {
            throw new NotImplementedException();
        }
    }
}

