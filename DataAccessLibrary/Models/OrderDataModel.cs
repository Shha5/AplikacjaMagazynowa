namespace DataAccessLibrary.Models
{
    public class OrderDataModel
    {
        public int Id { get; set; } = 0;
        public string OrderNumber { get; set; } = string.Empty;
        public string OrderSignature { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
        public List<OrderItemDataModel> OrderItems { get; set; }
    }
}
