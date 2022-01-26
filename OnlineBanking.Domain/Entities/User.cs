using System;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces;

namespace OnlineBanking.Domain.Entities
{
   public class User: IdentityUser, IEntity
    {
       
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        //public bool HasDefaultPassword { get; set; }
    }
}
