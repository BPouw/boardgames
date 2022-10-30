using System;
using Core.DomainServices.Service;
using Core.Domain;
using Moq;

namespace Core.DomainServices.Test
{
    public class CreateReviewTest
    {
        [Fact]
        public async Task Can_Not_Review_Your_Own_GamenightAsync()
        {
            // Arrange
            var personReviewRepository = new Mock<IPersonReviewRepository>();
            var reviewRepository = new Mock<IReviewRepository>();

            ReviewService reviewService = new ReviewService(personReviewRepository.Object, reviewRepository.Object);

            Review review = new Review()
            {
                Id = 1,
                Rating = 5,
                ReviewerId = 1,
                ReviewText = "A review text",
            };

            PersonReview personReview = new PersonReview()
            {
                PersonId = 1,
                ReviewId = 1
            };

            DomainException e = new DomainException();

            //Act
            try
            {
                await reviewService.CreateReview(review, personReview);
            } catch(DomainException exception)
            {
                e = exception;
            }

            //Assert
            Assert.Equal("You can not review yourself", e.Message);
        }

        [Fact]
        public async Task Can_Review_A_GamenightAsync()
        {
            // Arrange
            var personReviewRepository = new Mock<IPersonReviewRepository>();
            var reviewRepository = new Mock<IReviewRepository>();

            ReviewService reviewService = new ReviewService(personReviewRepository.Object, reviewRepository.Object);

            Review review = new Review()
            {
                Id = 1,
                Rating = 5,
                ReviewerId = 4,
                ReviewText = "A review text",
            };

            PersonReview personReview = new PersonReview()
            {
                PersonId = 1,
                ReviewId = 1
            };

            DomainException e = null;

            //Act
            try
            {
                await reviewService.CreateReview(review, personReview);
            }
            catch (DomainException exception)
            {
                e = exception;
            }

            //Assert
            Assert.Null(e);
        }
    }
}

