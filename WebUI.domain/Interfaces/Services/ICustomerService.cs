using OnlineBanking.Domain.Entities;
using WebUI.domain.Models;

namespace WebUI.domain.Interfaces.Services
{
   public interface ICustomerService
   {
       void Add(CustomerViewModel model);
       public Customer GetCustomer(string userId);
       public Customer GetCustomerWithAccount(string accountId);
    }
}
