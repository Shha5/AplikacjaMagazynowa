using Autofac.Extras.Moq;
using DataAccessLibrary.Models;

namespace AplikacjaMagazynowaAPI_Tests.TestHelpers
{
    public static class StubsHelper
    {
        static Random random;
        static AutoMock autoMock;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        const string stubProductName = "SampleProduct";

        static StubsHelper()
        {
            random = new Random();
            autoMock = AutoMock.GetLoose();
        }

        public static string GenerateRandomString()
        {
            int length = random.Next(5, 49);
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateStubOrderSignature()
        {
            return $"{(random.Next(1, 200))}/{random.Next(1, 13)}/{random.Next(2023, 2124)}";
        }

        public static List<OrderItemInputModel> GenerateStubValidOrderItemInputList()
        {
            var result = new List<OrderItemInputModel>();
            for (int i = 0; i < random.Next(3, 21);  i++)
            {
                var mockItem = autoMock.Mock<OrderItemInputModel>().SetupAllProperties();
                mockItem.Object.ProductCode = GenerateRandomString();
                mockItem.Object.Quantity = random.Next(1, 11);
                result.Add(mockItem.Object);
            }
            return result;
        }

        public static List<OrderItemOutputModel> GenerateStubValidOrderItemOutputList()
        {
            var coinFlip = random.Next(0, 2);
            var result = new List<OrderItemOutputModel>();
            for (int i = 0; i < random.Next(3, 21); i++)
            {
                var mockItem = autoMock.Mock<OrderItemOutputModel>().SetupAllProperties();
                mockItem.Object.ProductCode = GenerateRandomString();
                mockItem.Object.Quantity = random.Next(1, 11);
                if (coinFlip != 0)
                {
                    mockItem.Object.ItemCompleted = true;
                }
                else
                {
                    mockItem.Object.ItemCompleted = false;
                }
                result.Add(mockItem.Object);
            }
            return result;
        }

        public static OrderOutputModel GenerateStubValidOrderOutput() 
        {
            var mockOrder = autoMock.Mock<OrderOutputModel>().SetupAllProperties();
            mockOrder.Object.Id = random.Next(1,10000);
            mockOrder.Object.OrderNumber = Guid.NewGuid().ToString();
            mockOrder.Object.OrderSignature = GenerateStubOrderSignature();
            mockOrder.Object.OrderDetails = GenerateStubValidOrderItemOutputList();

            return mockOrder.Object;
        }

        public static EditOrderItemInputModel GenerateStubEditOrderInput(bool isQuantityValid = true)
        {
            var mockOrderEdit = autoMock.Mock<EditOrderItemInputModel>().SetupAllProperties();
            mockOrderEdit.Object.OrderNumber = Guid.NewGuid().ToString();
            mockOrderEdit.Object.ProductCode = GenerateRandomString();
            if (isQuantityValid == true)
            {
                mockOrderEdit.Object.NewQuantity = random.Next(20, 31);
            }
            else
            {
                mockOrderEdit.Object.NewQuantity = -(random.Next(1, 11));
            }
            return mockOrderEdit.Object;
        }

        public static ProductInputModel GenerateStubProductInput(bool isQuantityValid = true)
        {
            var mockProduct = autoMock.Mock<ProductInputModel>().SetupAllProperties();
            mockProduct.Object.ProductName = stubProductName;
            mockProduct.Object.ProductCode = GenerateRandomString();
            if (isQuantityValid == true)
            {
                mockProduct.Object.QuantityInStock = random.Next(0, 100);
            }
            else
            {
               mockProduct.Object.QuantityInStock = -(random.Next(1, 100));
            }
            return mockProduct.Object;
        }

        public static ShipmentInputModel GenerateStubShipmentInput(bool isQuantityValid = true) 
        {
            var shipmentMock = autoMock.Mock<ShipmentInputModel>().SetupAllProperties();
            shipmentMock.Object.ProductCode = GenerateRandomString();
            if (isQuantityValid == true)
            {
                shipmentMock.Object.Quantity = random.Next(1, 100);
            }
            else
            {
                shipmentMock.Object.Quantity = -(random.Next(0, 100));
            }

            return shipmentMock.Object;
        }

        public static List<ProductOutputModel> GenerateValidStubProductOutputList()
        {
            var result = new List<ProductOutputModel>();
            for (int i = 0; i < random.Next(3, 21); i++)
            {
                var mockProduct = autoMock.Mock<ProductOutputModel>().SetupAllProperties();
                mockProduct.Object.ProductCode = GenerateRandomString();
                mockProduct.Object.QuantityInStock = random.Next(0, 1000);
                mockProduct.Object.ProductName = string.Concat(stubProductName, i.ToString());
                mockProduct.Object.CreatedDate = DateTime.UtcNow.AddDays(-(random.Next(51, 100)));
                mockProduct.Object.LastModifiedDate = DateTime.UtcNow.AddDays(-(random.Next(1, 51)));
               
                result.Add(mockProduct.Object);
            }
            return result;
        }

        public static IEnumerable<ProductDataModel> GenerateValidStubProductDataList()
        {
            var result = new List<ProductDataModel>();
            for (int i = 0; i < random.Next(3, 21); i++)
            {
                var mockProduct = autoMock.Mock<ProductDataModel>().SetupAllProperties();
                mockProduct.Object.ProductCode = GenerateRandomString();
                mockProduct.Object.QuantityInStock = random.Next(0, 1000);
                mockProduct.Object.ProductName = string.Concat(stubProductName, i.ToString());
                mockProduct.Object.CreatedDate = DateTime.UtcNow.AddDays(-(random.Next(51, 100)));
                mockProduct.Object.LastModifiedDate = DateTime.UtcNow.AddDays(-(random.Next(1, 51)));

                result.Add(mockProduct.Object);
            }
            return result;
        }

        public static ProductDataModel GenerateValidStubProductData()
        {
            var mockProduct = autoMock.Mock<ProductDataModel>().SetupAllProperties();
            mockProduct.Object.ProductCode = GenerateRandomString();
            mockProduct.Object.QuantityInStock = random.Next(100, 1000);
            mockProduct.Object.ProductName = stubProductName;
            mockProduct.Object.CreatedDate = DateTime.UtcNow.AddDays(-(random.Next(51, 100)));
            mockProduct.Object.LastModifiedDate = DateTime.UtcNow.AddDays(-(random.Next(1, 51)));

            return mockProduct.Object;
        }

        public static ProductAvailabilityDataModel GenerateStubAvailabilityData(bool isInStock = true)
        {
            var mockAvailability = autoMock.Mock<ProductAvailabilityDataModel>().SetupAllProperties();
            mockAvailability.Object.Id = random.Next(1, 1000);
            if (isInStock == true)
            {
                mockAvailability.Object.QuantityInStock = random.Next(100, 1000);
            }
            else
            {
                mockAvailability.Object.QuantityInStock = 0;
            }
            return mockAvailability.Object;
        }

        public static OrderItemDataModel GenerateStubOrderItemData(bool isCompleted = false)
        {
            var mockOrderItemData = autoMock.Mock<OrderItemDataModel>().SetupAllProperties();
            mockOrderItemData.Object.Id = random.Next(1, 1000);
            mockOrderItemData.Object.OrderId = random.Next(1, 1000);
            mockOrderItemData.Object.ProductId = random.Next(1, 1000);
            mockOrderItemData.Object.ProductName = stubProductName;
            mockOrderItemData.Object.ProductCode = GenerateRandomString();
            mockOrderItemData.Object.Quantity = random.Next(1, 11);
            mockOrderItemData.Object.ItemCompleted = isCompleted;

            return mockOrderItemData.Object;
        }

        public static IEnumerable<OrderItemDataModel> GenerateStubOrderItemDataList()
        {
            var coinFlip = random.Next(0, 2);
            var result = new List<OrderItemDataModel>();
            for (int i = 0; i < random.Next(3, 21); i++)
            {
                var mockItem = autoMock.Mock<OrderItemDataModel>().SetupAllProperties();
                mockItem.Object.ProductCode = GenerateRandomString();
                mockItem.Object.Quantity = random.Next(1, 11);
                if (coinFlip != 0)
                {
                    mockItem.Object.ItemCompleted = true;
                }
                else
                {
                    mockItem.Object.ItemCompleted = false;
                }
                result.Add(mockItem.Object);
            }
            return result;
        }

        public static OrderDataModel GenerateStubOrderData()
        {
            var mockOrderData = autoMock.Mock<OrderDataModel>().SetupAllProperties();
            mockOrderData.Object.Id = random.Next(1, 1000);
            mockOrderData.Object.OrderNumber = Guid.NewGuid().ToString();
            mockOrderData.Object.CreatedDate = DateTime.UtcNow.AddDays(-(random.Next(11, 21)));
            mockOrderData.Object.LastModifiedDate = DateTime.UtcNow.AddDays(-(random.Next(1, 11)));
            mockOrderData.Object.OrderSignature = GenerateStubOrderSignature();
            
            return mockOrderData.Object;
        }
    }
}
