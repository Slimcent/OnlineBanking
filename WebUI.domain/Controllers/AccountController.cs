using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using WebUI.domain.Models;

namespace WebUI.domain.Controllers
{
   [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        //private readonly RoleManager<AppRole> _roleManager;
        
        public AccountController(UserManager<User> userManager, SignInManager<User> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
            //_roleManager = roleManager;
        }

        
        public IActionResult Index()
        {
            //User user = await _userManager.GetUserAsync(HttpContext.User);
            //string message = "Hello " + user.UserName + user.Email;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    await _signManager.SignOutAsync();
                    var roles = await _userManager.GetRolesAsync(user);
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                    
                        if (roles.Contains("SuperAdmin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (roles.Contains("Moderator"))
                        {
                            return RedirectToAction("UserHome", "Dashboard");
                        }
                        else
                        {
                            return RedirectToAction("CustomerHome", "Dashboard");
                        }
                    }
                }
                ModelState.AddModelError(nameof(loginViewModel.Email), "Login Failed: Invalid Email or Password");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
