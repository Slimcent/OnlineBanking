using System;
using System.ComponentModel.DataAnnotations;


namespace WebUI.domain.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="First Name canot be empty"), MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name canot be empty"), MaxLength(50), MinLength(2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName cannot be empty"), MinLength(2), MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email canot be empty"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthday cannot be empty")]
        public DateTime Birthday { get; set; }

        //[Phone]
        //[Display(Name = "Phone Number")]
        //public string PhoneNumber { get; set; }
    }
}
