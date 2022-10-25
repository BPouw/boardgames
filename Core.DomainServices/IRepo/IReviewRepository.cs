using System;
using Core.Domain;
namespace Core.DomainServices
{
    public interface IReviewRepository
    {
        Task AddReview(Review Review);
    }
}

