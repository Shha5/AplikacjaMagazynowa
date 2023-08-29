using System.ComponentModel.DataAnnotations;

namespace AplikacjaMagazynowaAPI.Models.InputModels
{
    public class OrderItemInputModel
    {
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
