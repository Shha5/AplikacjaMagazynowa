namespace AplikacjaMagazynowaAPI.Models.OutputModels
{
    public class OrderItemOutputModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public bool ItemCompleted { get; set; }
    }
}
