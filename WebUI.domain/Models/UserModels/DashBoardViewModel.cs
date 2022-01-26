using OnlineBanking.Domain.Entities;
using System.Collections.Generic;

namespace WebUI.domain.Models
{
    public class DashBoardViewModel
    {
        public User User { get; set; }        
        public OnlineBanking.Domain.Entities.Account Account { get; set; }
        public IEnumerable<Transaction> Transaction { get; set; }
    }
}
