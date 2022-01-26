using System.ComponentModel.DataAnnotations;

namespace WebUI.domain.Models.Password
{
    public class ResetPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password cannot be Empty"), DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}