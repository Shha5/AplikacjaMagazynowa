

namespace DataAccessLibrary.Models
{
    public class OrderItemDataModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public bool ItemCompleted { get; set; } = false;

    }
}
