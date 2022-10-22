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

        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr, INotyfService toastNotification, IPersonRepository personRepository)
        {
            userManager = userMgr;
            signInManager = signInMgr;
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

