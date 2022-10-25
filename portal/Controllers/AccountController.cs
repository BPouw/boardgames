using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Domain;
using Core.DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using portal.Models;

namespace portal.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private INotyfService _toastNotification;
        private IPersonRepository _personRepository;
        private IAddressRepository _addressRepository;
        private IPersonValidator _personValidator;

        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr, INotyfService toastNotification, IPersonRepository personRepository, IAddressRepository addressRepository, IPersonValidator personValidator)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            _toastNotification = toastNotification;
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _personValidator = personValidator;
            IdentitySeedData.EnsurePopulated(userMgr).Wait();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {

            var user = await userManager.FindByNameAsync(loginModel.Name);

            if (user != null)
            {
                await signInManager.SignOutAsync();
                if ((await signInManager.PasswordSignInAsync(user,
                    loginModel.Password, false, false)).Succeeded)
                {
                    return Redirect("/GameNight/Index");
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel loginModel)
        {
     
            var user = await userManager.FindByNameAsync(loginModel.Email);

            if (user != null)
                {
                _toastNotification.Error("User already exists", 10);
                    return View();
                }

            if (!_personValidator.CheckAgeMinimumSixteen(loginModel.DateOfBirth))
            {
                _toastNotification.Error("You must be at least 16 years old", 10);
                return View();
            }

            if (ModelState.IsValid)
            {
                user = new IdentityUser(loginModel.Email);

                await userManager.CreateAsync(user, loginModel.Password);

                Person person = new Person();
                Address address = new Address();

                person.Name = loginModel.Name;
                person.Email = loginModel.Email;
                person.DateOfBirth = loginModel.DateOfBirth;
                person.Gender = loginModel.Gender;

                address.StreetName = loginModel.StreetName;
                address.City = loginModel.City;
                address.HouseNumber = loginModel.HouseNumber;
                address.PostalCode = loginModel.PostalCode;

                await _addressRepository.CreateAddress(address);

                person.AddressId = address.Id;
                person.Address = address;
                person.Vegan = loginModel.Vegan;
                person.LactoseIntolerant = loginModel.LactoseIntolerant;
                person.NutAllergy = loginModel.NutAllergy;
                person.AlcoholFree = loginModel.AlcoholFree;

                await _personRepository.CreatePerson(person);

                _toastNotification.Information("Succesfully registered", 10);

                return Redirect("/Account/Login");

            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            return View();
        }

    }
}

