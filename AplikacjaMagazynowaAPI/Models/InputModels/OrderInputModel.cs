using System.ComponentModel.DataAnnotations;

namespace AplikacjaMagazynowaAPI.Models.InputModels
{
    public class OrderInputModel
    {
        [Required]
        public List<OrderItemInputModel> Items { get; set; }
    }
}
