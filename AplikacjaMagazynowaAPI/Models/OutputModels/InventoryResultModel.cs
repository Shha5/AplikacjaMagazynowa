using System.ComponentModel.DataAnnotations;

namespace AplikacjaMagazynowaAPI.Models.OutputModels
{
    public class InventoryResultModel
    {
        [Required]
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
