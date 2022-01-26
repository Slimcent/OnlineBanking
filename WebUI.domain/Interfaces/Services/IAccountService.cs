using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;

namespace WebUI.domain.Interfaces.Services
{
    public interface IAccountService
    {
        Account GetAccount(string accountNumber);
        public Account Get(Guid Id);
        void checkBalance(User user);

        //int Update(UpdateViewModel model);

    }
}
