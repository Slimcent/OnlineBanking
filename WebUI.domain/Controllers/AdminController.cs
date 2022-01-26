using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Folder;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Model;
using WebUI.domain.Models;
using WebUI.domain.Models.Account;

namespace WebUI.domain.Controllers
{

    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ICustomerService customerService;
        private readonly IPasswordHasher<User> passwordHasher;
        private IPasswordValidator<User> passwordValidator;
        private IUserValidator<User> userValidator;
        public AdminController(UserManager<User> _userManager, RoleManager<AppRole> _roleManager, 
            ICustomerService _customerService, IPasswordHasher<User> _passwordHasher, 
            IPasswordValidator<User> _passwordVal, IUserValidator<User> _userVal)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            customerService = _customerService;
            passwordHasher = _passwordHasher;
            passwordValidator = _passwordVal;
            userValidator = _userVal;
        }
        public IActionResult Index()
        {
            User user = userManager.GetUserAsync(HttpContext.User).Result;
            //ViewBag.Message = $"Welcome {user.FullName}";
            return View(userManager.Users);
        }

        public async Task<(IdentityResult, User)> UserCreate(UserViewModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = $"{model.FirstName} {model.LastName}",
                Birthday = model.Birthday,
                CreatedAt = System.DateTime.Now
            };
            var password = "123456";
            IdentityResult result = await userManager.CreateAsync(user, password);
            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                AppRole role = new AppRole();
                role.Name = "Moderator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                if (!roleResult.Succeeded)
                {
                    ModelState.AddModelError("", "Error while creating Role");
                }
            }
            await userManager.AddToRoleAsync(user, "Moderator");
            return (result, user);
        }

        [HttpGet]
        public ViewResult CreateUser() => View(new UserViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var result = await UserCreate(model);
                
                if (result.Item1.Succeeded)
                    return RedirectToAction("Index");                   

                ModelState.AddModelError("", "User creation failed");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        private async Task<(IdentityResult result, User user)> CustomerCreate(IdentityViewModel model)
        {
            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
               
            };
            var password = "123456";
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (!roleManager.RoleExistsAsync("Customer").Result)
                {
                    AppRole role = new AppRole();
                    role.Name = "Customer";
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Error while creating Role");
                    }
                }
                await userManager.AddToRoleAsync(user, "Customer");
            }
            return (result, user);
        }

        [HttpGet]
        public ViewResult CreateCustomer() => View(new CustomerViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateCustomer(CustomerViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var (result, user) = await CustomerCreate(new IdentityViewModel
                { Email = model.Email, FullName = $"{model.FirstName} {model.LastName}" });
                if (result.Succeeded)
                {
                    model.ReadOnlyCustomerProps = new ReadOnlyCustomerProps
                    {
                        UserId = user.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    customerService.Add(model);
                    //TempData["Success"] = "User Creation was Successful!";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string name, string email, string password)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult validName = null;

                if (!string.IsNullOrEmpty(name))
                {

                    validName = await userValidator.ValidateAsync(userManager, user);
                    if (validName.Succeeded)
                        user.Email = email;
                    else
                        Errors(validName);
                }
                else
                {
                    ModelState.AddModelError("", "Name cannot be empty");
                }

                IdentityResult validEmail = null;
                if (!string.IsNullOrEmpty(email))
                {
                    validEmail = await userValidator.ValidateAsync(userManager, user);
                    if (validEmail.Succeeded)
                        user.Email = email;
                    else
                        Errors(validEmail);
                }
                else
                {
                    ModelState.AddModelError("", "Email cannot be empty");
                }

              

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User not found");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User not found");
            return View("Index", userManager.Users);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
