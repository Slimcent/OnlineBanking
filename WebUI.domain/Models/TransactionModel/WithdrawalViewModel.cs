using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Models.TransactionModel
{
    public class WithdrawalViewModel
    {
        [Required(ErrorMessage = "Amount cannot be empty"), Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "500", "140000", ErrorMessage = "Withdrawal amount must be between $500 - $140000")]
        public decimal Amount { get; set; }
        
        [BindNever]
        public Customer Customer { get; set; }
    }
}
