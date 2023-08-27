namespace AplikacjaMagazynowaAPI.Models
{
    public class ProductModel
    {
        public int Id { get; set; } = 0;
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}
