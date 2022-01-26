using System.ComponentModel.DataAnnotations;


namespace WebUI.domain.Models
{
    public class RolesVIewModel
    {
        [Required(ErrorMessage ="Enter Role Name")]
        public string RoleName { get; set; }

        [Required(ErrorMessage ="indicate who created ths Role")]
        public string CreatedBy { get; set; }
    }
}
