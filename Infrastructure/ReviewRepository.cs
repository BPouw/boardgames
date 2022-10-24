using System;
using Core.DomainServices;
using Core.Domain;

namespace Infrastructure
{
    public class ReviewRepository: IReviewRepository
    {
        BoardgamesContext _context;

        public ReviewRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public async Task AddReview(Review Review)
        {
             _context.Add(Review);
            await _context.SaveChangesAsync();
        }
    }
}

