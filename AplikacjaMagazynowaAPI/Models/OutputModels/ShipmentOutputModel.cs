namespace AplikacjaMagazynowaAPI.Models.OutputModels
{
    public class ShipmentOutputModel
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}
