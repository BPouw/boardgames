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

        public async Task DeleteGameNight(GameNight gameNight)
        {
            _context.GameNight.Remove(gameNight);
            await _context.SaveChangesAsync();
        }

        public GameNight getGameNightById(int id)
        {
            return _context.GameNight.Where(p => p.Id == id).Include(g => g.Organiser).Include(g => g.Players).First();
        }

        public GameNight getGameNightPopulated(int id)
        {
            return _context.GameNight.Where(p => p.Id == id).Include(g => g.Games).Include(g => g.Players).Include(g => g.Address).Include(g => g.Organiser).First();
        }

        public IEnumerable<GameNight> getGameNights()
        {
            return _context.GameNight.Include(g => g.Games).Include(g => g.Organiser).OrderBy(g => g.DateTime);
        }

        public IEnumerable<GameNight> getGameNightsByOrganiser(int id)
        {
            return _context.GameNight.Where(p => p.OrganiserId == id).OrderBy(g => g.DateTime);
        }

        public IEnumerable<GameNight> getJoinedGameNights(Person person)
        {
            return _context.GameNight.Where(p => p.Players.Contains(person)).Include(g => g.Organiser).OrderBy(g => g.DateTime);
        }

        public bool HasJoinedGameNightOnDay(Person person, DateTime date)
        {
            if (_context.GameNight.Count(p => p.Players.Contains(person) && p.DateTime.Date == date.Date) == 0)
            {
                return false;
            }
            return true;
        }

        public async Task UpdateGameNight(GameNight gameNight)
        {

            var local = _context.Set<GameNight>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(gameNight.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(gameNight).State = EntityState.Modified;

            // save 
            _context.SaveChanges();

        }
    }
}