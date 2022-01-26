using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Folder;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Models;

namespace WebUI.domain.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepo )
        {
            _unitOfWork = unitOfWork;
            _customerRepo = customerRepo;
        }

        public void Add(CustomerViewModel model)
        {
            var emailExists = EmailExists(model.Email);
            if(emailExists != null)
            {
                var customer = new Customer
                {
                    UserId = model.ReadOnlyCustomerProps.UserId,
                    Birthday = model.Birthday,
                    //PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    AccountType = model.AccountType,

                    Account = new Account()
                    {
                        AccountType = model.AccountType,
                        AccountNumber = AccountNumberGenerator.GenerateAccountNumber(),
                        CreatedBy = model.FirstName,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Balance = model.AccountType != AccountType.Savings ? 0 : 5000,
                        IsActive = true,
                    }
                };
                _customerRepo.Add(customer);
                _unitOfWork.Commit();
            }
            else
            {
                var msg = "Email already Exists in the database";
            }
            
        }
        
        public Customer GetCustomer(string userId)
        {
            return _customerRepo.Find(a => a.UserId == userId).FirstOrDefault();
        }

        public Customer GetCustomerWithAccount(string accountId)
        {
            return _customerRepo.Find(a => a.AccountId.ToString() == accountId).FirstOrDefault();
        }

        private Customer EmailExists(string email)
        {
            return _customerRepo.Find(e => e.User.Email == email).SingleOrDefault();
        }
    }
}
