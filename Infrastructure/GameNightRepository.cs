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
            _context.GameNight.Add(GameNight);
            await _context.SaveChangesAsync();
        }

        public GameNight getGameNightById(int id)
        {
            return _context.GameNight.Where(p => p.Id == id).First();
        }

        public GameNight getGameNightPopulated(int id)
        {
            return _context.GameNight.Where(p => p.Id == id).Include(g => g.Games).Include(g => g.Players).Include(g => g.Address).First();
        }

        public IQueryable<GameNight> getGameNights()
        {
            return _context.GameNight.Include(g => g.Games).Include(g => g.Players);
        }

        public IEnumerable<GameNight> getGameNightsByOrganiser(int id)
        {
            return _context.GameNight.Where(p => p.OrganiserId == id);
        }


    }
}