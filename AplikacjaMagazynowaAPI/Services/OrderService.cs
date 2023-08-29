using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Models;
using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;
using System.Data;

namespace AplikacjaMagazynowaAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductData _productData;
        private readonly IOrderData _orderData;
        public OrderService(IProductData productData, IOrderData orderData)
        {
            _productData = productData;
            _orderData = orderData;
        }

        public async Task<OrderResultModel> CreateOrder(OrderInputModel order)
        {
            var productAvailability = await CheckProductsAvailability(order.Items);
            if (productAvailability.Any(p => p.IsAvailable == false))
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.ProductUnavailable);
            }
            string orderSignature = await AssignOrderSignature();
            string orderNumber = Guid.NewGuid().ToString();
            await _orderData.InsertOrder(new OrderDataModel { OrderSignature = orderSignature, OrderNumber = orderNumber });
            int orderId = await _orderData.GetOrderIdByOrderNumber(orderNumber);
            await SaveOrderItems(order.Items, orderId);
            if (await GetOrderItems(orderId) == null)
            {
                await _orderData.DeleteOrder(orderId);
                return GenerateUnsuccessfulOrderResult(ErrorMessages.UnexpectedServerError);
            }
            return new OrderResultModel()
            {
                Success = true,
                OrderNumber = orderNumber,
                OrderSignature = orderSignature
            };
        }

        public async Task<OrderResultModel> DeleteOrderItem(string orderNumber, string productCode)
        {
            var orderItem = await _orderData.GetOrderItemByOrderNumberAndProductCode(orderNumber, productCode);
            if (orderItem == null)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.OrderItemDoesNotExist);
            }
            if (orderItem.ItemCompleted == true)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.CannotEditComplete); 
            }
            await _orderData.DeleteOrderItem(orderItem);
            return GenerateEmptySuccessfulOrderResult();
        }

        public async Task<OrderResultModel> EditOrderItem(EditOrderItemInputModel orderItemEdit)
        {
            var orderItem = await _orderData.GetOrderItemByOrderNumberAndProductCode(orderItemEdit.OrderNumber, orderItemEdit.ProductCode);

            if (orderItem == null)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.OrderItemDoesNotExist);     
            }
            if (orderItem.ItemCompleted == true)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.CannotEditComplete);
            }
            if (orderItem.Quantity == orderItemEdit.NewQuantity)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.NoChangeInData);
            }
            var productData = await _productData.GetProductDetailsByProductId(orderItem.ProductId);
            if ((productData.QuantityInStock + orderItem.Quantity) < orderItemEdit.NewQuantity)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.ProductUnavailable);
            }
            EditOrderItemDataModel orderItemEditData = new EditOrderItemDataModel()
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                OrderId = orderItem.OrderId,
                NewQuantity = orderItemEdit.NewQuantity,
                QuantityDifference = orderItem.Quantity - orderItemEdit.NewQuantity
            };
            await _orderData.EditOrderItem(orderItemEditData);
            return GenerateEmptySuccessfulOrderResult();
        }

        public async Task<OrderOutputModel> GetOrderByOrderNumber(string orderNumber)
        {
            var orderData = await _orderData.GetOrderByOrderNumber(orderNumber);
            if (orderData == null || orderData.Id == 0) 
            {
                return null;
            }
            var orderItems = await GetOrderItems(orderData.Id);
            return new OrderOutputModel()
            {
                OrderNumber = orderNumber,
                Id = orderData.Id,
                OrderSignature = orderData.OrderSignature,
                OrderDetails = orderItems
            };
        }

        public async Task<OrderResultModel> MarkOrderItemComplete(string orderNumber, string productCode)
        {
            var orderItem = await _orderData.GetOrderItemByOrderNumberAndProductCode(orderNumber, productCode);
            if (orderItem == null)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.OrderItemDoesNotExist);
            }
            if (orderItem.ItemCompleted == true)
            {
                return GenerateUnsuccessfulOrderResult(ErrorMessages.NoChangeInData);
            }
            await _orderData.MarkOrderItemComplete(orderNumber, productCode);
            return GenerateEmptySuccessfulOrderResult();
        }

        private async Task<string> AssignOrderSignature()
        {
            DateTime currentDay = DateTime.Today;
            int orderCount = await _orderData.GetNumberOfOrdersInAGivenMonth();
            return $"{(orderCount + 1)}/{currentDay.Month}/{currentDay.Year}";
        }

        private async Task<List<ProductAvailabilityModel>> CheckProductsAvailability(List<OrderItemInputModel> orderDetails)
        {
            var result = new List<ProductAvailabilityModel>();
            foreach (var orderDetail in orderDetails)
            {
                var productDetails = await _productData.GetProductDetailsByProductCode(orderDetail.ProductCode);
                if (productDetails == null)
                {
                    result.Add(new ProductAvailabilityModel
                    {
                        ProductCode = orderDetail.ProductCode,
                        Id = 0,
                        IsAvailable = false,
                    });
                    break;
                }
                if (productDetails.QuantityInStock < orderDetail.Quantity)
                {
                    result.Add(new ProductAvailabilityModel
                    {
                        ProductCode = orderDetail.ProductCode,
                        Id = productDetails.Id,
                        IsAvailable = false,

                    });
                    break;
                }
                else
                {
                    result.Add(new ProductAvailabilityModel
                    {
                        ProductCode = orderDetail.ProductCode,
                        Id = productDetails.Id,
                        IsAvailable = true
                    });
                }
            }
            return result;
        }

        private OrderResultModel GenerateEmptySuccessfulOrderResult()
        {
            return new OrderResultModel()
            {
                Success = true
            };
        }

        private OrderResultModel GenerateUnsuccessfulOrderResult(string ErrorMessage)
        {
            return new OrderResultModel()
            {
                Success = false,
                Error = ErrorMessage
            };
        }

        private async Task<List<OrderItemOutputModel>> GetOrderItems(int orderId)
        {
            var orderItemData = await _orderData.GetOrderItemsByOrderId(orderId);
            if (orderItemData == null)
            {
                return null;
            }
            List<OrderItemOutputModel> orderItems = new List<OrderItemOutputModel>();
            foreach (var item in orderItemData)
            {
                var product = await _productData.GetProductDetailsByProductId(item.ProductId);
                orderItems.Add(new OrderItemOutputModel()
                {
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    Quantity = item.Quantity,
                    ItemCompleted = item.ItemCompleted

                });
            }
            return orderItems;
        }

        private async Task SaveOrderItems(List<OrderItemInputModel> orderItems, int orderId)
        {
            foreach (var item in orderItems)
            {
                await _orderData.InsertOrderItems(new OrderItemDataModel()
                {
                    OrderId = orderId,
                    ProductCode = item.ProductCode,
                    Quantity = item.Quantity,
                });
            }
        }   
    }
}
