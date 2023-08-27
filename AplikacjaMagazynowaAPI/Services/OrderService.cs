using AplikacjaMagazynowaAPI.Models;
using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;

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

        public async Task<OrderOutputModel> GetOrderByOrderNumber(string orderNumber)
        {
            var orderData = await _orderData.GetOrderByOrderNumber(orderNumber);
            List<OrderItemOutputModel> orderItems = new List<OrderItemOutputModel>();
            foreach (var item in orderData.OrderItems)
            {
                orderItems.Add(new OrderItemOutputModel()
                {
                    ProductCode = item.ProductCode,
                    Quantity = item.Quantity,
                    ItemCompleted = item.ItemCompleted,
                });
            }
            return new OrderOutputModel()
            {
                OrderNumber = orderNumber,
                Id = orderData.Id,
                OrderSignature = orderData.OrderSignature,
                OrderDetails = orderItems
            };
        }

        public async Task MarkOrderItemComplete(string orderNumber, string productCode)
        {
            await _orderData.MarkOrderItemComplete(orderNumber, productCode);
        }

        public async Task<OrderResultModel> SaveOrder(List<OrderItemInputModel> orderItems)
        {
            var productAvailability = await CheckProductsAvailability(orderItems);
            if (productAvailability.Any(p => p.IsAvailable == false))
            {
                return new OrderResultModel()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Jeden lub więcej produktów z Twojego zamówienia nie jest dostępny w żądanej ilości lub nie istnieje w bazie."
                    }
                };
            }
            string orderSignature = await AssignOrderSignature();
            string orderNumber = Guid.NewGuid().ToString();
            await _orderData.InsertOrder(new OrderDataModel { OrderSignature = orderSignature, OrderNumber = orderNumber });
            int orderId = await _orderData.GetOrderIdByOrderNumber(orderNumber);
            await SaveOrderItems(orderItems, orderId);
            return new OrderResultModel()
            {
                Success = true,
                OrderNumber = orderNumber,
                OrderSignature = orderSignature
            };
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
