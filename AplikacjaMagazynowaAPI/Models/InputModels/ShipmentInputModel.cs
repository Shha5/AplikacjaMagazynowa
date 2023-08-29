using System.ComponentModel.DataAnnotations;

namespace AplikacjaMagazynowaAPI.Models.InputModels
{
    public class ShipmentInputModel
    {
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
