
namespace OnlineBanking.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public int PlotNo { get; set; }
       
        public string StreetName { get; set; }
       
        public string City { get; set; }
        
        public string State { get; set; }
       
        public string Nationality { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
