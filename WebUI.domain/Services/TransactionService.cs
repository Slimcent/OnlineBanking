using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using System;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Models.TransactionModel;
using OnlineBanking.Domain.Enumerators;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepo;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;

        public TransactionService(IUnitOfWork unitOfWork, ITransactionRepository transactionRepo, 
            ICustomerService customerService, IAccountService accountService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _transactionRepo = transactionRepo;
            _accountService = accountService;
            _customerService = customerService;
            _userManager = userManager;
        }

        public async Task<(bool success, string message)> Deposit(DepositViewModel model)
        {
            var account = _accountService.Get(model.Customer.AccountId);
            var CanDeposit = model.Amount >= 500;
            var isAccountActive = account?.IsActive;
            if (!CanDeposit || !isAccountActive.Value)
            {
                var msg = !isAccountActive.Value ? "This Account is inactive, Please visit the bank to Re-activate" :
                                                    "You can only deposit from 500 and above ";
                return (false, msg);
            }
            account.Balance += model.Amount;
            var debit = new Transaction()
            {
                Amount = model.Amount,
                TimeStamp = DateTime.Now,
                TransactionTypes = TransactionType.Credit,
                UserId = model.Customer.UserId
            };
            _transactionRepo.Add(debit);
            var result = await _unitOfWork.CommitAsync();
            return result > 0 ? (true, $"Your Deposit of {model.Amount} was Successful") : 
                                (false, "Deposit was unsuccessful, Please try again");
        }

        public async Task<(bool success, string message)> Withdrawal(WithdrawalViewModel model)
        {
            var account = _accountService.Get(model.Customer.AccountId);
            var WithdrawalLimit = account.Balance > 1000;
            var CanWithdraw = account.Balance > model.Amount;
            var isAccountActive = account.IsActive;

            var msg = !WithdrawalLimit ? "You must have at least 1000 in your Account" :
                      !isAccountActive ? "You cannot Withdraw while your Account is Inactive" :
                      !CanWithdraw ? "You cannot Withdraw below your Balance" :
                      string.Empty;
            if (!string.IsNullOrEmpty(msg)) return (false, msg);
            
            account.Balance -= model.Amount;
            var withdaw = new Transaction()
            {
                Amount = model.Amount,
                TimeStamp = DateTime.Now,
                TransactionTypes = TransactionType.Debit,
                UserId = model.Customer.UserId
            };
            _transactionRepo.Add(withdaw);
            var result = await _unitOfWork.CommitAsync();
            return result > 0 ? (true, $"Your Withdrawal of {model.Amount} was Successful") :
                                (false, "Withdrawal was unsuccessful, Please try again");
        }

        public async Task<(bool success, string message)> Transfer(TransferViewModel model)
        {
            var SendersAccount = _accountService.Get(model.Customer.AccountId);
            var recipientAccount = _accountService.GetAccount(model.ReciepientAccountNumber);

            var TransferLimit = SendersAccount.Balance > 1000;
            var isSendersAccountActive = SendersAccount.IsActive;
            var IsrecipientAccountActive = recipientAccount.IsActive;
            var isReciepientAccountTheSame = recipientAccount?.AccountNumber != SendersAccount?.AccountNumber;
            var canTransfer = SendersAccount.Balance > model.Amount;

            var msg = !TransferLimit ? "You must have more than 1000 in your Account" :
                      !isSendersAccountActive ? "You cannot make a transfer while your Account is Inactive" :
                      !IsrecipientAccountActive ? "You cannot transfer to an Inactive Account" :
                      !isReciepientAccountTheSame ? "You cannot transfer to yourself" :
                      !canTransfer ? "Your Balanc is Insufficient" :
                      string.Empty;
            if (!string.IsNullOrEmpty(msg)) return (false, msg);

            SendersAccount.Balance -= model.Amount;
            recipientAccount.Balance += model.Amount;

            var SenderTransaction = new Transaction()
            {
                Amount = model.Amount,
                TimeStamp = DateTime.Now,
                TransactionTypes = TransactionType.Debit,
                UserId = model.Customer.UserId
            };
            var RecipientTransaction = new Transaction()
            {
                Amount = model.Amount,
                TimeStamp = DateTime.Now,
                TransactionTypes = TransactionType.Credit,
                UserId = model.Customer.UserId
            };
            _transactionRepo.Add(RecipientTransaction);
            _transactionRepo.Add(SenderTransaction);

            var result = await _unitOfWork.CommitAsync();
            return result > 0 ? (true, $"Your Transfer of {model.Amount} was Successful") : 
                (false, "Transfer was unsuccessful, Please try again");            
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _transactionRepo.GetAll();
        }

        public IEnumerable<Transaction> GetTransactionForAUser(string id)
        {
           return _transactionRepo.Find(t => t.UserId == id).ToList();
        }

        
    }
}
