
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

        public Task InsertOrder(OrderDataModel order) => _dbAccess.SaveData("sp_Order_Insert", new { order.OrderSignature, order.OrderNumber });


        public async Task<int> GetNumberOfOrdersInAGivenMonth()
        {
            var currentMonthRange = _dateTimeHelper.GetCurrentMonthRange();
            var OrderCount = await _dbAccess.LoadData<int, DateRangeModel>("sp_Order_GetNumberOfOrdersInAGivenMonth", currentMonthRange);
            return OrderCount.FirstOrDefault();
        }

        public Task InsertOrderItems(OrderItemDataModel orderItem) =>
             _dbAccess.SaveData("sp_OrderItems_Insert", 
                 new { orderItem.OrderId, orderItem.ProductCode, orderItem.Quantity });

        public async Task<int> GetOrderIdByOrderNumber(string orderNumber)
        {
            var orderIdentifiers = await _dbAccess.LoadData<int, dynamic>("sp_Order_GetOrderIdByOrderNumber", new { orderNumber });
            return orderIdentifiers.FirstOrDefault();
        }

        public Task MarkOrderItemComplete(string orderNumber, string productCode) =>
            _dbAccess.SaveData("sp_OrderItems_MarkComplete", new { orderNumber, productCode });

        public async Task<OrderDataModel> GetOrderByOrderNumber(string orderNumber)
        {
            var result = await _dbAccess.LoadData<OrderDataModel, dynamic>("sp_Order_GetOrderByOrderNumber", new { orderNumber });
            return result.FirstOrDefault();
        }
    }
}
