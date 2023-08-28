using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data.Interfaces
{
    public interface IOrderData
    {
        Task DeleteOrder(int orderId);
        Task DeleteOrderItem(OrderItemDataModel orderItem);
        Task EditOrderItem(EditOrderItemDataModel orderItemEdit);
        Task<int> GetNumberOfOrdersInAGivenMonth();
        Task<OrderDataModel> GetOrderByOrderNumber(string orderNumber);
        Task<OrderItemDataModel> GetOrderItemByOrderNumberAndProductCode(string orderNumber, string productCode);
        Task<int> GetOrderIdByOrderNumber(string orderNumber);
        Task<IEnumerable<OrderItemDataModel>> GetOrderItemsByOrderId(int orderId);
        Task InsertOrder(OrderDataModel order);
        Task InsertOrderItems(OrderItemDataModel orderDetail);
        Task MarkOrderItemComplete(string orderNumber, string productCode);
    }
}