using DataAccessLibrary.Constants;
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

        public Task DeleteOrder(int orderId) => _dbAccess.SaveData(StoredProceduresNames.spOrderDelete, new { orderId });

        public Task DeleteOrderItem(OrderItemDataModel orderItem) => _dbAccess.SaveData(StoredProceduresNames.spOrderItemDelete,
            new { orderItem.OrderId, orderItem.ProductId, orderItem.Id, orderItem.Quantity });

        public Task EditOrderItem(EditOrderItemDataModel orderItemEdit) => _dbAccess.SaveData(StoredProceduresNames.spOrderItemsEdit,
            new { orderItemEdit.Id, orderItemEdit.ProductId, orderItemEdit.OrderId, orderItemEdit.NewQuantity, orderItemEdit.QuantityDifference });

        public async Task<int> GetNumberOfOrdersInAGivenMonth()
        {
            var currentMonthRange = _dateTimeHelper.GetCurrentMonthRange();
            var OrderCount = await _dbAccess.LoadData<int, DateRangeModel>(StoredProceduresNames.spOrderGetNumberOfOrdersInAMonth, currentMonthRange);
            return OrderCount.FirstOrDefault();
        }

        public async Task<OrderDataModel> GetOrderByOrderNumber(string orderNumber)
        {
            var result = await _dbAccess.LoadData<OrderDataModel, dynamic>(StoredProceduresNames.spOrderGetOrderDetailsByOrderNumber, new { orderNumber });
            return result.FirstOrDefault();
        }

        public async Task<int> GetOrderIdByOrderNumber(string orderNumber)
        {
            var orderIdentifiers = await _dbAccess.LoadData<int, dynamic>(StoredProceduresNames.spOrderGetOrderIdByOrderNumber, new { orderNumber });
            return orderIdentifiers.FirstOrDefault();
        }

        public async Task<OrderItemDataModel> GetOrderItemByOrderNumberAndProductCode(string orderNumber, string productCode)
        {
            var result = await _dbAccess.LoadData<OrderItemDataModel, dynamic>(StoredProceduresNames.spOrderGetOrderItemByOrderNumberAndProductCode, new { orderNumber, productCode });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderItemDataModel>> GetOrderItemsByOrderId(int orderId)
        {
            return await _dbAccess.LoadData<OrderItemDataModel, dynamic>(StoredProceduresNames.spOrderGetOrderItemsByOrderId, new { orderId });
        }

        public Task InsertOrder(OrderDataModel order) => _dbAccess.SaveData(StoredProceduresNames.spOrderInsert, new { order.OrderSignature, order.OrderNumber });

        public Task InsertOrderItems(OrderItemDataModel orderItem) =>
             _dbAccess.SaveData(StoredProceduresNames.spOrderItemsInsert, 
                 new { orderItem.OrderId, orderItem.ProductCode, orderItem.Quantity });

        public Task MarkOrderItemComplete(string orderNumber, string productCode) =>
            _dbAccess.SaveData(StoredProceduresNames.spOrderItemsMarkComplete, new { orderNumber, productCode });
    }
}
