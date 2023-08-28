namespace DataAccessLibrary.Models
{
    public class EditOrderItemDataModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int NewQuantity { get; set; }
        public int QuantityDifference { get; set; }
    }
}

