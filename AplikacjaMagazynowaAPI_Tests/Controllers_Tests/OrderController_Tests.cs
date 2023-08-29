using AplikacjaMagazynowaAPI.Controllers;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaMagazynowaAPI_Tests.Controllers_Tests
{
    [TestFixture]
    internal class OrderController_Tests : TestBase<OrderController>
    {
        private const string sampleError = "Sample Error";
        private string stubOrderNumber = Guid.NewGuid().ToString();
        private string stubProductCode = StubsHelper.GenerateRandomString();
        private List<OrderItemInputModel> orderItemsInputStub = StubsHelper.GenerateStubValidOrderItemInputList();
        private readonly OrderResultModel stubUnsuccessfullOrderResult = new OrderResultModel() { Success = false, Error = sampleError };
        private readonly OrderResultModel stubSuccessfullOrderResult = new OrderResultModel() { Success = true };

        [Test]
        public async Task CreateNewOrder_OrderServiceReturnsFalse_ReturnsBadRequest()
        {
            var mockOrder = AutoMock.Mock<OrderInputModel>().SetupAllProperties();
            mockOrder.Object.Items = orderItemsInputStub;
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.CreateOrder(It.IsAny<OrderInputModel>()))
                .ReturnsAsync(stubUnsuccessfullOrderResult);

            var result = await SystemUnderTest.CreateNewOrder(mockOrder.Object);

            result.ShouldBeOfType<BadRequestObjectResult>();    
        }

        [Test]
        public async Task CreateNewOrder_OrderServiceReturnsTrue_ReturnsOk()
        {
            var mockOrder = AutoMock.Mock<OrderInputModel>().SetupAllProperties();
            mockOrder.Object.Items = orderItemsInputStub;
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.CreateOrder(It.IsAny<OrderInputModel>()))
                .ReturnsAsync(new OrderResultModel() { Success = true });

            var result = await SystemUnderTest.CreateNewOrder(mockOrder.Object);

            result.ShouldBeOfType<OkObjectResult>();
        }

        [Test]
        public async Task DeleteOrderItem_OrderServiceReturnsFalse_ReturnsBadRequest()
        {
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.DeleteOrderItem(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(stubUnsuccessfullOrderResult);

            var result = await SystemUnderTest.DeleteOrderItem(stubOrderNumber, stubProductCode);

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task DeleteOrderItem_OrderServiceReturnsTrue_ReturnsOk()
        {
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.DeleteOrderItem(It.IsAny<string>(), It.IsAny<string>()))
               .ReturnsAsync(stubSuccessfullOrderResult);

            var result = await SystemUnderTest.DeleteOrderItem(stubOrderNumber, stubProductCode);

            result.ShouldBeOfType<OkResult>();
        }

        [Test]
        public async Task EditOrderItem_ItemQuantityIsLessThanOr0_ReturnsBadRequest()
        {
            var invalidOrderEdit = StubsHelper.GenerateStubEditOrderInput(false);

            var result = await SystemUnderTest.EditOrderItem(invalidOrderEdit);

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task EditOrderItem_OrderServiceReturnsFalse_ReturnsBadRequest()
        {
            var validOrderEditStub = StubsHelper.GenerateStubEditOrderInput();
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.EditOrderItem(It.IsAny<EditOrderItemInputModel>()))
                .ReturnsAsync(stubUnsuccessfullOrderResult);

            var result = await SystemUnderTest.EditOrderItem(validOrderEditStub);

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task EditOrderItem_OrderServiceReturnsTrue_ReturnsOk()
        {
            var validOrderEditStub = StubsHelper.GenerateStubEditOrderInput();
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.EditOrderItem(It.IsAny<EditOrderItemInputModel>()))
                .ReturnsAsync(stubSuccessfullOrderResult);

            var result = await SystemUnderTest.EditOrderItem(validOrderEditStub);

            result.ShouldBeOfType<OkResult>();
        }


        [Test]
        public async Task GetOrderByOrderNumber_OrderServiceReturnsNull_ReturnsNotFound()
        { 
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.GetOrderByOrderNumber(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var result = await SystemUnderTest.GetOrderByOrderNumber(stubOrderNumber);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetOrderByOrderNumber_OrderServiceReturnsResult_ReturnsOk()
        {
            var validOrderStub = StubsHelper.GenerateStubValidOrderOutput(); 
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.GetOrderByOrderNumber(It.IsAny<string>()))
               .ReturnsAsync(validOrderStub);

            var result = await SystemUnderTest.GetOrderByOrderNumber(stubOrderNumber);

            result.ShouldBeOfType<OkObjectResult>();
        }

        [Test]
        public async Task MarkOrderItemComplete_OrderServiceReturnsFalse_ReturnsBadRequest()
        {
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.MarkOrderItemComplete(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(stubUnsuccessfullOrderResult);

            var result = await SystemUnderTest.MarkOrderItemComplete(stubOrderNumber, stubProductCode);

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task MarkOrderItemComplete_OrderServiceReturnsTrue_ReturnsOk()
        {
            var mockOrderService = AutoMock.Mock<IOrderService>().Setup(x => x.MarkOrderItemComplete(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(stubSuccessfullOrderResult);

            var result = await SystemUnderTest.MarkOrderItemComplete(stubOrderNumber, stubProductCode);

            result.ShouldBeOfType<OkResult>();

        }
    }
}
