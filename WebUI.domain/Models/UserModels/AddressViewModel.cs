using OnlineBanking.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace WebUI.domain.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage ="Plot Number cannot be empty")]
        public int PlotNo { get; set; }

        [Required(ErrorMessage = "Street Name cannot be empty"), MaxLength(50), MinLength(2)]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "City Name cannot be empty"), MaxLength(30), MinLength(2)]
        public string City { get; set; }

        [Required(ErrorMessage = "State Name cannot be empty"), MaxLength(30), MinLength(2)]
        public string State { get; set; }

        [Required(ErrorMessage = "Nationality cannot be empty"), MaxLength(30), MinLength(4)]
        public string Nationality { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
