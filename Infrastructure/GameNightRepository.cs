using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GameNightRepository : IGameNightRepository
    {

        private readonly BoardgamesContext _context;

        public GameNightRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public async Task AddGameNight(GameNight GameNight)
        {
            _context.GameNights.Add(GameNight);
            await _context.SaveChangesAsync();
        }

        public IQueryable<GameNight> getGameNights()
        {
            return _context.GameNights.Include(g => g.Games).Include(g => g.Players);
        }

        public IEnumerable<GameNight> getGameNightsByOrganiser(int id)
        {
            return _context.GameNights.Where(p => p.OrganiserId == id);
        }
    }
}