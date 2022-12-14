using Freemer.Identity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Freemer.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, 
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }



        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModelMy
            { 
                ReturnUrl = returnUrl 
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModelMy viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.UserName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.UserName,
                viewModel.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModelMy
            {
                ReturnUrl = returnUrl
            };

            return View(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModelMy viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser()
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Error occurred");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            //return Redirect(logoutRequest.PostLogoutRedirectUri);
            return RedirectToAction("Login", "Auth");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
