namespace AplikacjaMagazynowaAPI.Models
{
    public class ShipmentModel
    {
        public int Id { get; set; } = 0;
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
