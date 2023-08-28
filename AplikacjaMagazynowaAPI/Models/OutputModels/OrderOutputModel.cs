using AplikacjaMagazynowaAPI.Models.InputModels;

namespace AplikacjaMagazynowaAPI.Models.OutputModels
{
    public class OrderOutputModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderSignature { get; set; }
        public List<OrderItemOutputModel> OrderDetails { get; set; }
    }
}
