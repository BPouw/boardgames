using System;
using Core.Domain;

namespace Core.DomainServices.IService
{
    public interface IReviewService
    {
        Task CreateReview(Review review, PersonReview personReview);
    }
}

