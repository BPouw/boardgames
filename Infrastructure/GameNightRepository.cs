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

        public Task DeleteGameNight(GameNight gameNight)
        {
            throw new NotImplementedException();
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
            return _context.GameNight.Include(g => g.Games).Include(g => g.Organiser);
        }

        public IEnumerable<GameNight> getGameNightsByOrganiser(int id)
        {
            return _context.GameNight.Where(p => p.OrganiserId == id);
        }

        public IEnumerable<GameNight> getJoinedGameNights(Person person)
        {
            return _context.GameNight.Where(p => p.Players.Contains(person)).Include(g => g.Organiser);
        }

        public Task UpdateGameNight(GameNight gameNight)
        {
            throw new NotImplementedException();
        }
    }
}