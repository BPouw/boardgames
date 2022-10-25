using System;
using Core.Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Webservice
{
    public class Query
    {

        private readonly BoardgamesContext _boardgamesDbContext;

        public Query(IDbContextFactory<BoardgamesContext> contextFactory)
        {
            _boardgamesDbContext = contextFactory.CreateDbContext();
        }

        public IEnumerable<GameNight> GetGameNightsWithGames() =>
            _boardgamesDbContext.GameNight.Include(g => g.Games).Include(g => g.Players).Include(g => g.Address);
    }

}
