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
    public class TransferViewModel
    {
        [Required(ErrorMessage = "Account number cannot be Empty")]
        [MinLength(10, ErrorMessage = "Invalid Account Number! Account Number must be 10 digits")]
        [MaxLength(10, ErrorMessage = "Invalid Account Number! Account Number must be 10 digits")]
        public string ReciepientAccountNumber { get; set; }

        [Required(ErrorMessage = "Amount cannot be Empty"), Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "500", "1000000", ErrorMessage = "Transfer amount must be between $500 - $1000000")]
        public decimal Amount { get; set; }

        [BindNever]
        public Customer Customer { get; set; }
    }
}
