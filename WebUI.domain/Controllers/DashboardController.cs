using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Middlewares;
using WebUI.domain.Models;

namespace WebUI.domain.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private UserManager<User> userManager;
        private readonly IAccountService accountService;
        private readonly ICustomerService customerService;
        private readonly ITransactionService transactionService;
        private readonly IDateTime dateTime;
        public DashboardController(UserManager<User> userMgr, IAccountService _accountService, 
            ICustomerService _customerService, ITransactionService _transactionService, IDateTime _dateTime)
        {
            userManager = userMgr;
            accountService = _accountService;
            customerService = _customerService;
            transactionService = _transactionService;
            dateTime = _dateTime;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var UserId = await userManager.GetUserAsync(HttpContext.User);
            var model = new DashBoardViewModel
            {
                User = UserId,
                
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult UserHome()
        {
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> CustomerHome()
        {
            var userId = await userManager.FindByIdAsync(HttpContext.User.GetUserId());
            var customer = customerService.GetCustomer(userId.Id);
            var account = accountService.Get(customer.AccountId);
            var userTransactins = transactionService.GetTransactionForAUser(userId.Id);
            var totalTransactions = transactionService.GetTransactionForAUser(userId.Id).ToList().Count();

            var serverTime = dateTime.Greeting;
            if (serverTime.Hour < 12)
            {
                ViewData["Greeting"] = "Good Morning";
            }
            else if (serverTime.Hour < 17)
            {
                ViewData["Greeting"] = "Good Afternoon";
            }
            else
            {
                ViewData["Greeting"] = "Good Evening";
            }
            
            var model = new DashBoardViewModel
            {
                User = userId,
                Account = account,
                Transaction = userTransactins
            };
            return View(model);
        }
    }
}
