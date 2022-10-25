using System;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IPersonReviewRepository
    {
        Task AddPersonToReview(PersonReview PersonReview);
        IEnumerable<PersonReview> GetReviewsForPerson(int personId);
        double AverageScoreForPerson(int personId);
    }
}

