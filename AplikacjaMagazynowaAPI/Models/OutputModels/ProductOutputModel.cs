using System.ComponentModel.DataAnnotations;

namespace AplikacjaMagazynowaAPI.Models.OutputModels
{
    public class ProductOutputModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
