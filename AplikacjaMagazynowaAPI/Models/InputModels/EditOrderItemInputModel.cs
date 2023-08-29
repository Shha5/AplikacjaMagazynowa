using System.ComponentModel.DataAnnotations;

namespace AplikacjaMagazynowaAPI.Models.InputModels
{
    public class EditOrderItemInputModel
    {
        [Required]
        public int NewQuantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }
    }
}
