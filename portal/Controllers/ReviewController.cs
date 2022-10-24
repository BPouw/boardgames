using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using portal.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace portal.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewRepository _reviewRepository;
        private IPersonReviewRepository _personReviewRepository;
        private INotyfService _toastNotification;

        public ReviewController(IReviewRepository reviewRepository, IPersonReviewRepository personReviewRepository, INotyfService toastNotification)
        {
            this._reviewRepository = reviewRepository;
            this._personReviewRepository = personReviewRepository;
            this._toastNotification = toastNotification;
        }


        [HttpGet]
        public IActionResult CreateReview()
        {
            var model = new CreateReviewViewModel();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review();
                PersonReview personReview = new PersonReview();
                review.ReviewText = reviewViewModel.ReviewText;
                review.Rating = reviewViewModel.Rating;

                //review.ReviewText = "Dit is een test joey&danine";
                //review.Rating = 5;

                review.ReviewerId = (int)HttpContext.Session.GetInt32("ReviewerId");

                personReview.PersonId = (int)HttpContext.Session.GetInt32("OrganiserId");

                if (personReview.PersonId == review.ReviewerId)
                {
                    _toastNotification.Error("You can not review yourself", 10);
                    return Redirect("/GameNight/Index");
                }

                await _reviewRepository.AddReview(review);

                personReview.ReviewId = review.Id;

                await _personReviewRepository.AddPersonToReview(personReview);
                return Redirect("/GameNight/Index");
            }
            return View(reviewViewModel);
        }

        public IActionResult Reviews()
        {
            int id = (int)HttpContext.Session.GetInt32("LoggedInPersonId");
            return View(_personReviewRepository.GetReviewsForPerson(id).ToViewModel());
        }

    }
}

