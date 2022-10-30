using System;
using Core.Domain;
using Core.DomainServices.IService;

namespace Core.DomainServices.Service
{
    public class ReviewService: IReviewService
    {
        private IPersonReviewRepository _personReviewRepository;
        private IReviewRepository _reviewRepository;

        public ReviewService(IPersonReviewRepository personReviewRepository, IReviewRepository reviewRepository)
        {
            this._personReviewRepository = personReviewRepository;
            this._reviewRepository = reviewRepository;
        }

        public async Task CreateReview(Review review, PersonReview personReview)
        {
            if (personReview.PersonId == review.ReviewerId)
            {
                throw new DomainException("You can not review yourself");
            }

            await _reviewRepository.AddReview(review);
            personReview.ReviewId = review.Id;
            await _personReviewRepository.AddPersonToReview(personReview);
        }
    }
}

