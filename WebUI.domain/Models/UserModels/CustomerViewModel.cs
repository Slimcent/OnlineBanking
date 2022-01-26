using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBanking.Domain.Enumerators;
using WebUI.domain.Model;

namespace WebUI.domain.Models
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "First Name cannot be empty"), MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be empty"), MinLength(3), MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName cannot be empty"), MinLength(2), MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email cannot be empty"), EmailAddress]
        public string Email { get; set; }

        public AccountType AccountType { get; set; }

        [Required(ErrorMessage = "Birthday cannot be empty"), CustomBirthday]
        public DateTime Birthday { get; set; }

        //[Phone]
        //[Display(Name = "Phone Number")]
        //public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Choose A Gender")]
        public Gender Gender { get; set; }

        [BindNever]
        public ReadOnlyCustomerProps ReadOnlyCustomerProps { get; set; }
    }
}
