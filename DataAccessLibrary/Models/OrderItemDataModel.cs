

namespace DataAccessLibrary.Models
{
    public class OrderItemDataModel
    {
        public int OrderId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public bool ItemCompleted { get; set; } = false;

    }
}
