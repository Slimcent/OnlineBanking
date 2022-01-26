using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebUI.domain.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty"), DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
