using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Models;

namespace WebUI.domain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;

        
        public HomeController(ICustomerService  customerService, IAddressService addressService)
        {
            _customerService = customerService;
            _addressService = addressService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Password()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        [HttpPost]
        public int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(CustomerViewModel customerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                _customerService.Add(customerModel);
                return View("Address");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
                return View();
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Address( AddressViewModel addressModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }                 
                _addressService.Add(addressModel);
                return View("Thanks");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}
