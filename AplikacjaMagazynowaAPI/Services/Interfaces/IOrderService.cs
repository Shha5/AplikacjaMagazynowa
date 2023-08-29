using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;

namespace AplikacjaMagazynowaAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResultModel> DeleteOrderItem(string orderNumber, string productCode);
        Task<OrderResultModel> EditOrderItem(EditOrderItemInputModel orderItemEdit);
        Task<OrderOutputModel> GetOrderByOrderNumber(string orderNumber);
        Task<OrderResultModel> MarkOrderItemComplete(string orderNumber, string productCode);
        Task<OrderResultModel> CreateOrder(OrderInputModel order);
    }
}