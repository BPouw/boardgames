using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Domain;
using Core.DomainServices;
using Core.DomainServices.IService;
using Core.DomainServices.Service;
using Microsoft.AspNetCore.Mvc;
using portal.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace portal.Controllers
{
    public class ReviewController : Controller
    {
        private INotyfService _toastNotification;
        private IReviewService _reviewService;
        private IPersonReviewRepository _personReviewRepository;

        public ReviewController(INotyfService toastNotification, IReviewService reviewService, IPersonReviewRepository personReviewRepository)
        {
            this._toastNotification = toastNotification;
            this._personReviewRepository = personReviewRepository;
            this._reviewService = reviewService;
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

                review.ReviewerId = HttpContext.Session.GetInt32("ReviewerId");

                personReview.PersonId = HttpContext.Session.GetInt32("OrganiserId");

                try
                {
                    await _reviewService.CreateReview(review, personReview);
                    _toastNotification.Success("Created review", 10);
                } catch(DomainException e)
                {
                    _toastNotification.Error(e.Message, 10);
                } 

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

