using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.domain.Models;

namespace WebUI.domain.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly UserManager<User> userManager;
               
        public RolesController(RoleManager<AppRole> _roleManager, UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolesVIewModel rolesVIewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppRole role = new AppRole
                    {
                        Name = rolesVIewModel.RoleName,
                        CreatedBy = rolesVIewModel.CreatedBy,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                        
                    };
                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("", "Role cannot be created");
                        Errors(result);
                }
                return View(rolesVIewModel);
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
            try
            {
                var role = await roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                    return View("NotFound");
                }

                var edit = new EditRoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    
                };

                foreach (var user in userManager.Users)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        edit.Users.Add(user.UserName);
                    }
                }
                return View(edit);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AppRole role = await roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    IdentityResult result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
                else
                    ModelState.AddModelError("", "No Role found");
                return View("Index", roleManager.Roles);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
