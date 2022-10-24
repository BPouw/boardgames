using System;
using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class PersonReviewRepository: IPersonReviewRepository
    {
        private BoardgamesContext _context;

        public PersonReviewRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public async Task AddPersonToReview(PersonReview PersonReview)
        {
            _context.Person_Review.Add(PersonReview);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PersonReview> GetReviewsForPerson(int personId)
        {
            return _context.Person_Review.Where(r => r.PersonId == personId).Include(g => g.Review).Include(g => g.Person).Include(g => g.Review.Reviewer);
        }

        public double AverageScoreForPerson(int personId)
        {
           return _context.Person_Review.Where(r => r.PersonId == personId).Include(g => g.Review).Average(g => g.Review.Rating);
        }
    }
}
