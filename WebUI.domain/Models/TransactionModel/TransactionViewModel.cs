using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Models
{
    public class TransactionViewModel
    {
        public int TransactionID { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public TransactionType TransactionTypes { get; set; }

        [Required]
        [Display(Name = "SenderAccount Number")]
        public int SenderAccountNumber { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Display(Name = "Recipient Account")]
        public string RecipientAccount { get; set; }

        [Column(TypeName = "decimal(38,2)")]
        public double Amount { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
