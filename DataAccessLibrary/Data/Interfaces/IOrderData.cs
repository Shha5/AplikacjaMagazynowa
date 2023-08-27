using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data.Interfaces
{
    public interface IOrderData
    {
        Task InsertOrder(OrderDataModel order);
        Task<int> GetNumberOfOrdersInAGivenMonth();
        Task InsertOrderItems(OrderItemDataModel orderDetail);
        Task<int> GetOrderIdByOrderNumber(string orderNumber);
        Task MarkOrderItemComplete(string orderNumber, string productCode);
        Task<OrderDataModel> GetOrderByOrderNumber(string orderNumber);
    }
}