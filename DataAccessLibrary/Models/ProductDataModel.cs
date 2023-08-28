namespace DataAccessLibrary.Models
{
    public class ProductDataModel
    {
        public int Id { get; set; } = 0;
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; set;} = DateTime.UtcNow;
    }
}
