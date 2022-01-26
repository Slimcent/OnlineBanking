using OnlineBanking.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using WebUI.domain.Interfaces.Services;
using OnlineBanking.Domain.Repositories;

using OnlineBanking.Domain.Interfaces.Repositories;

namespace WebUI.domain.Services
{
    public class AccountService: IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepo;

        public AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepo)
        {
            _unitOfWork = unitOfWork;
            _accountRepo = accountRepo;
        }

        public void checkBalance(User user)
        {
            throw new NotImplementedException();
        }

        public Account Get(Guid Id)
        {
            return _accountRepo.Find(a => a.Id == Id).FirstOrDefault();
        }
       
        public Account GetAccount(string accountNumber)
        {
            return _accountRepo.Find(a => a.AccountNumber == accountNumber).FirstOrDefault();
        }
    }
}
