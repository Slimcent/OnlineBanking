using OnlineBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebUI.domain.Models.TransactionModel;

namespace WebUI.domain.Interfaces.Services
{
    public interface ITransactionService
    {
        public Task<(bool success, string message)> Deposit(DepositViewModel model);
        public Task<(bool success, string message)> Withdrawal(WithdrawalViewModel model);
        public Task<(bool success, string message)> Transfer(TransferViewModel model);
        public IEnumerable<Transaction> GetAll();
        public IEnumerable<Transaction> GetTransactionForAUser(string id);
    }
}
