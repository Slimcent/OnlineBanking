using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using System;
using System.Threading.Tasks;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Middlewares;
using WebUI.domain.Models.TransactionModel;

namespace WebUI.domain.Controllers
{
    public class TransactionController:Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;

        public TransactionController(UserManager<User> userManager, RoleManager<AppRole> roleManager, 
            SignInManager<User> signInManager, IAccountService accountService, 
            ITransactionService transactionService, ICustomerService customerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _accountService =accountService;
            _transactionService = transactionService;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Withdraw(WithdrawalViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
                    model.Customer = _customerService.GetCustomer(HttpContext.User.GetUserId());

                    var (success, message) = await _transactionService.Withdrawal(model);
                    if (!success) { ModelState.AddModelError("", message); return View(); }

                    TempData["WithdrawalSuccess"] = message;
                    return RedirectToAction("CustomerHome", "Dashboard");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(DepositViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                var userId = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
                model.Customer = _customerService.GetCustomer(HttpContext.User.GetUserId());

                var (success, message) = await _transactionService.Deposit(model);

                if (!success) { ModelState.AddModelError("", message); return View(); }

                TempData["DepositSuccess"] = message;
                return View();
                //return RedirectToAction("CustomerHome", "Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                var userId = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
                model.Customer = _customerService.GetCustomer(HttpContext.User.GetUserId());

                var (success, message) = await _transactionService.Transfer(model);
                if (!success) { ModelState.AddModelError("", message); return View(); }

                TempData["TransferSuccess"] = message;
                return RedirectToAction("CustomerHome", "Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}
