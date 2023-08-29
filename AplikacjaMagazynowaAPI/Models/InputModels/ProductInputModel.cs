using System.ComponentModel.DataAnnotations;

namespace AplikacjaMagazynowaAPI.Models.InputModels
{
    public class ProductInputModel
    {
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        public int QuantityInStock { get; set; }
    }
}
