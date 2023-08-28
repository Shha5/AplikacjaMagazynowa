using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Helpers.Interfaces;
using DataAccessLibrary.Models;
using DataAccessLibrary.SqlAccess.Interfaces;

namespace DataAccessLibrary.Data
{
    public class OrderData : IOrderData
    {
        private readonly ISqlDataAccess _dbAccess;
        private readonly IDateTimeHelper _dateTimeHelper;

        public OrderData(ISqlDataAccess dbAccess, IDateTimeHelper dateTimeHelper)
        {
            _dbAccess = dbAccess;
            _dateTimeHelper = dateTimeHelper;
        }

        public Task DeleteOrder(int orderId) => _dbAccess.SaveData("sp_Order_Delete", new { orderId });

        public Task DeleteOrderItem(OrderItemDataModel orderItem) => _dbAccess.SaveData("sp_OrderItem_DeleteOrderItem",
            new { orderItem.OrderId, orderItem.ProductId, orderItem.Id, orderItem.Quantity });

        public Task EditOrderItem(EditOrderItemDataModel orderItemEdit) => _dbAccess.SaveData("sp_OrderItems_EditOrderItem",
            new { orderItemEdit.Id, orderItemEdit.ProductId, orderItemEdit.OrderId, orderItemEdit.NewQuantity, orderItemEdit.QuantityDifference });

        public async Task<int> GetNumberOfOrdersInAGivenMonth()
        {
            var currentMonthRange = _dateTimeHelper.GetCurrentMonthRange();
            var OrderCount = await _dbAccess.LoadData<int, DateRangeModel>("sp_Order_GetNumberOfOrdersInAGivenMonth", currentMonthRange);
            return OrderCount.FirstOrDefault();
        }

        public async Task<OrderDataModel> GetOrderByOrderNumber(string orderNumber)
        {
            var result = await _dbAccess.LoadData<OrderDataModel, dynamic>("sp_Order_GetOrderByOrderNumber", new { orderNumber });
            return result.FirstOrDefault();
        }

        public async Task<int> GetOrderIdByOrderNumber(string orderNumber)
        {
            var orderIdentifiers = await _dbAccess.LoadData<int, dynamic>("sp_Order_GetOrderIdByOrderNumber", new { orderNumber });
            return orderIdentifiers.FirstOrDefault();
        }

        public async Task<OrderItemDataModel> GetOrderItemByOrderNumberAndProductCode(string orderNumber, string productCode)
        {
            var result = await _dbAccess.LoadData<OrderItemDataModel, dynamic>("sp_OrderItems_GetOrderItemByOrderNumberAndProductCode", new { orderNumber, productCode });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderItemDataModel>> GetOrderItemsByOrderId(int orderId)
        {
            return await _dbAccess.LoadData<OrderItemDataModel, dynamic>("sp_OrderItems_GetOrderItemsByOrderId", new { orderId });
        }

        public Task InsertOrder(OrderDataModel order) => _dbAccess.SaveData("sp_Order_Insert", new { order.OrderSignature, order.OrderNumber });

        public Task InsertOrderItems(OrderItemDataModel orderItem) =>
             _dbAccess.SaveData("sp_OrderItems_Insert", 
                 new { orderItem.OrderId, orderItem.ProductCode, orderItem.Quantity });

        public Task MarkOrderItemComplete(string orderNumber, string productCode) =>
            _dbAccess.SaveData("sp_OrderItems_MarkComplete", new { orderNumber, productCode });
    }
}
