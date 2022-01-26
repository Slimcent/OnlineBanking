using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Email cannot be empty"), EmailAddress, MaxLength(50)]
        public string Email { get; set; }
    }
}
