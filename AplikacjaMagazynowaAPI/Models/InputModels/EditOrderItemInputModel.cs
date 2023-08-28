namespace AplikacjaMagazynowaAPI.Models.InputModels
{
    public class EditOrderItemInputModel
    {
        public int NewQuantity { get; set; }
        public string OrderNumber { get; set; }
        public string ProductCode { get; set; }
    }
}
