using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBanking.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.domain.Models.TransactionModel
{
    public class DepositViewModel
    {
        [Required(ErrorMessage = "Amount cannot be empty"), Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "500", "5000000000", ErrorMessage = "Deposit amount must be between $500 - $5000000000")]
        public decimal Amount { get; set; }
        [BindNever]
        public Customer Customer { get; set; }
    }
}
