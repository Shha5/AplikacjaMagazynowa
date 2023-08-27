using Microsoft.Identity.Client;

namespace AplikacjaMagazynowaAPI.Models
{
    public class ProductAvailabilityModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsAvailable { get; set; }
    }
}
