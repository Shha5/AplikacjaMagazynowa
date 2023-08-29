using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Constants
{
    public class StoredProceduresNames
    {
        public const string spOrderDelete = "sp_Order_Delete";
        public const string spOrderGetNumberOfOrdersInAMonth = "sp_Order_GetNumberOfOrdersInAGivenMonth";
        public const string spOrderGetOrderDetailsByOrderNumber = "sp_Order_GetOrderDetailsByOrderNumber";
        public const string spOrderGetOrderIdByOrderNumber = "sp_Order_GetOrderIdByOrderNumber";
        public const string spOrderInsert = "sp_Order_Insert";
        public const string spOrderItemDelete = "sp_OrderItem_DeleteOrderItem";
        public const string spOrderItemsEdit = "sp_OrderItems_EditOrderItem";
        public const string spOrderGetOrderItemByOrderNumberAndProductCode = "sp_OrderItems_GetOrderItemByOrderNumberAndProductCode";
        public const string spOrderGetOrderItemsByOrderId = "sp_OrderItems_GetOrderItemsByOrderId";
        public const string spOrderItemsInsert = "sp_OrderItems_Insert";
        public const string spOrderItemsMarkComplete = "sp_OrderItems_MarkComplete";
        public const string spProductGetAll = "sp_Product_GetAll";
        public const string spProductGetProductDetailsById = "sp_Product_GetProductDetailsById";
        public const string spProductGetProductDetailsByProductCode = "sp_Product_GetProductDetailsByProductCode";
        public const string spProductInsert = "sp_Product_Insert";
        public const string spShipmentInsert = "sp_Shipment_InsertProductShipment";
    }
}
