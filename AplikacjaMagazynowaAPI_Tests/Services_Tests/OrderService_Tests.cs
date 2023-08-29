using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Services;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;



namespace AplikacjaMagazynowaAPI_Tests.Services_Tests
{
    [TestFixture]
    internal class OrderService_Tests : TestBase<OrderService>
    {
        private readonly string stubOrderNumber = Guid.NewGuid().ToString();
        private readonly string stubProductCode = StubsHelper.GenerateRandomString();
        private readonly EditOrderItemInputModel validStubOrderItemEditInput = StubsHelper.GenerateStubEditOrderInput();
        private readonly List<OrderItemInputModel> validStubOrderItemInputList = StubsHelper.GenerateStubValidOrderItemInputList();
        private readonly Random random = new Random();

        [Test]
        public async Task CreateOrder_OneOfProductsIsNull_ReturnsSuccessFalseAndErrorMessage()
        {
            var stubOrder = AutoMock.Mock<OrderInputModel>().SetupAllProperties();
            stubOrder.Object.Items = validStubOrderItemInputList;
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>())).ReturnsAsync(() => null);

            var result = await SystemUnderTest.CreateOrder(stubOrder.Object);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.ProductUnavailable);
        }

        [Test]
        public async Task CreateOrder_OneOfProductsStockIsLessThanOrdered_ReturnsSuccessFalseAndErrorMessage()
        {
            var stubOrder = AutoMock.Mock<OrderInputModel>().SetupAllProperties();
            stubOrder.Object.Items = validStubOrderItemInputList;
            var stubProductData = AutoMock.Mock<ProductAvailabilityDataModel>().SetupAllProperties();
            stubProductData.Object.Id = random.Next(1, 1000);
            stubProductData.Object.QuantityInStock = (validStubOrderItemInputList.First().Quantity - 1);
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>())).ReturnsAsync(stubProductData.Object);

            var result = await SystemUnderTest.CreateOrder(stubOrder.Object);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.ProductUnavailable);
        }

        [Test]
        public async Task CreateOrder_ProductsAvailableItemsSuccessfullySaved_ReturnsSuccessTrueAndOrderIdentifiers()
        {
            // ARRANGE
            var stubOrder = AutoMock.Mock<OrderInputModel>().SetupAllProperties();
            stubOrder.Object.Items = validStubOrderItemInputList;
            var stubProductAvailabilityData = StubsHelper.GenerateStubAvailabilityData();
            var stubProductData = StubsHelper.GenerateStubValidProductData();
            var stubOrderItemsData = StubsHelper.GenerateStubOrderItemDataIEnum();
            var mockProductData = AutoMock.Mock<IProductData>();
            mockProductData.Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>())).ReturnsAsync(stubProductAvailabilityData);
            mockProductData.Setup(p => p.GetProductDetailsByProductId(It.IsAny<int>())).ReturnsAsync(stubProductData);
            var mockOrderData = AutoMock.Mock<IOrderData>();
            mockOrderData.Setup(o => o.GetNumberOfOrdersInAGivenMonth()).ReturnsAsync(random.Next(0, 200));
            mockOrderData.Setup(o => o.GetOrderItemsByOrderId(It.IsAny<int>())).ReturnsAsync(stubOrderItemsData);
            mockOrderData.Setup(o => o.GetOrderIdByOrderNumber(It.IsAny<string>())).ReturnsAsync(random.Next(1, 1000));

            // ACT
            var result = await SystemUnderTest.CreateOrder(stubOrder.Object);

            // ASSERT 
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeTrue();
            result.Error.ShouldBeNullOrEmpty();
            result.OrderNumber.ShouldNotBeNullOrEmpty();
            result.OrderSignature.ShouldNotBeNullOrEmpty();
            result.OrderSignature.ShouldNotStartWith("0");
        }

        [Test]
        public async Task CreateOrder_SaveOrderItemsFailed_ReturnsSuccessFalseAndErrorMessage()
        {
            // ARRANGE
            var stubOrder = AutoMock.Mock<OrderInputModel>().SetupAllProperties();
            stubOrder.Object.Items = validStubOrderItemInputList;
            var stubProductAvailabilityData = StubsHelper.GenerateStubAvailabilityData();
            var mockProductData = AutoMock.Mock<IProductData>();
            mockProductData.Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>())).ReturnsAsync(stubProductAvailabilityData);
            mockProductData.Setup(p => p.GetProductDetailsByProductId(It.IsAny<int>())).ReturnsAsync(() => null);
            var mockOrderData = AutoMock.Mock<IOrderData>();
            mockOrderData.Setup(o => o.GetNumberOfOrdersInAGivenMonth()).ReturnsAsync(random.Next(0, 200));
            mockOrderData.Setup(o => o.GetOrderIdByOrderNumber(It.IsAny<string>())).ReturnsAsync(random.Next(1, 1000));
            mockOrderData.Setup(o => o.GetOrderItemsByOrderId(It.IsAny<int>())).ReturnsAsync(() => null);

            // ACT
            var result = await SystemUnderTest.CreateOrder(stubOrder.Object);

            // ASSERT 
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.UnexpectedServerError);

        }

        [Test]
        public async Task DeleteOrderItem_OrderItemDoesNotExist_ReturnsSuccessFalseAndErrorMessage()
        {
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var result = await SystemUnderTest.DeleteOrderItem(stubOrderNumber, stubProductCode);

            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.OrderItemDoesNotExist);
        }

        [Test]
        public async Task DeleteOrderItem_OrderItemExistsAndIsNotCompleted_ReturnsSuccessTrue()
        {
            var stubOrderItemNotCompleted = StubsHelper.GenerateStubOrderItemData(false);
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(stubOrderItemNotCompleted);

            var result = await SystemUnderTest.DeleteOrderItem(stubOrderNumber, stubProductCode);

            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeTrue();
            result.Error.ShouldBeNullOrEmpty();
        }

        [Test]
        public async Task DeleteOrderItem_OrderItemIsCompleted_ReturnsSuccessFalseAndErrorMessage()
        {
            var stubOrderItemCompleted = StubsHelper.GenerateStubOrderItemData(true);
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(stubOrderItemCompleted);

            var result = await SystemUnderTest.DeleteOrderItem(stubOrderNumber, stubProductCode);

            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.CannotEditComplete);
        }

        [Test]
        public async Task EditOrderItem_NewItemQuantityIsMoreThanInStock_ReturnsSuccessFalseAndErrorMessage()
        {
            // ARRANGE
            var stubOrderItemNotCompleted = StubsHelper.GenerateStubOrderItemData(false);
            var stubProductData = StubsHelper.GenerateStubValidProductData();
            var invalidQuantityStubOrderItemEdit = AutoMock.Mock<EditOrderItemInputModel>().SetupAllProperties();
            invalidQuantityStubOrderItemEdit.Object.ProductCode = StubsHelper.GenerateRandomString();
            invalidQuantityStubOrderItemEdit.Object.OrderNumber = Guid.NewGuid().ToString();
            invalidQuantityStubOrderItemEdit.Object.NewQuantity = (stubProductData.QuantityInStock + random.Next(11, 21));

            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>())).
                ReturnsAsync(stubOrderItemNotCompleted);
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductId(It.IsAny<int>())).
                ReturnsAsync(stubProductData);

            // ACT
            var result = await SystemUnderTest.EditOrderItem(invalidQuantityStubOrderItemEdit.Object);

            // ASSERT
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.ProductUnavailable);
        }

        [Test]
        public async Task EditOrderItem_NewItemQuantityGoodAndOrderItemExistsAndIsNotCompleted_ReturnsSuccessTrue()
        {
            // ARRANGE
            var stubOrderItemNotCompleted = StubsHelper.GenerateStubOrderItemData(false);
            var stubProductData = StubsHelper.GenerateStubValidProductData();

            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>())).
                ReturnsAsync(stubOrderItemNotCompleted);
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductId(It.IsAny<int>())).
                ReturnsAsync(stubProductData);

            // ACT
            var result = await SystemUnderTest.EditOrderItem(validStubOrderItemEditInput);

            // ASSERT
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeTrue();
            result.Error.ShouldBeNullOrEmpty();
        }

        [Test]
        public async Task EditOrderItem_NewItemQuantityIsTheSameAsExisting_ReturnsSuccessFalseAndErrorMessage()
        {
            var stubOrderItemNotCompleted = StubsHelper.GenerateStubOrderItemData(false);
            var invalidQuantityStubOrderItemEdit = AutoMock.Mock<EditOrderItemInputModel>().SetupAllProperties();
            invalidQuantityStubOrderItemEdit.Object.ProductCode = StubsHelper.GenerateRandomString();
            invalidQuantityStubOrderItemEdit.Object.OrderNumber = Guid.NewGuid().ToString();
            invalidQuantityStubOrderItemEdit.Object.NewQuantity = stubOrderItemNotCompleted.Quantity;

            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>())).
                ReturnsAsync(stubOrderItemNotCompleted);

            var result = await SystemUnderTest.EditOrderItem(invalidQuantityStubOrderItemEdit.Object);

            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.NoChangeInData);
        }

        [Test]
        public async Task EditOrderItem_OrderItemDoesNotExist_ReturnsSuccessFalseAndErrorMessage()
        {
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var result = await SystemUnderTest.EditOrderItem(validStubOrderItemEditInput);

            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.OrderItemDoesNotExist);
        }

        [Test]
        public async Task EditOrderItem_OrderItemIsCompleted_ReturnsSuccessFalseAndErrorMessage()
        {
            var stubOrderItemCompleted = StubsHelper.GenerateStubOrderItemData(true);
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>())).
                ReturnsAsync(stubOrderItemCompleted);

            var result = await SystemUnderTest.EditOrderItem(validStubOrderItemEditInput);

            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.CannotEditComplete);
        }

        [Test]
        public async Task GetOrderByOrderNumber_OrderDoesNotExist_ReturnsNull()
        {
            var mockOrderData = AutoMock.Mock<IOrderData>();
            mockOrderData.Setup(o => o.GetOrderByOrderNumber(It.IsAny<string>())).ReturnsAsync(() => null);

            var result = await SystemUnderTest.GetOrderByOrderNumber(stubOrderNumber);

            result.ShouldBeNull();
        }

        [Test]
        public async Task GetOrderByOrderNumber_OrderExists_ReturnsOrder()
        {
            // ARRANGE
            var stubOrderData = StubsHelper.GenerateStubOrderData();
            var stubOrderItemsData = StubsHelper.GenerateStubOrderItemDataIEnum();
            var stubProductData = StubsHelper.GenerateStubValidProductData();
            var mockOrderData = AutoMock.Mock<IOrderData>();
            mockOrderData.Setup(o => o.GetOrderByOrderNumber(It.IsAny<string>())).ReturnsAsync(stubOrderData);
            mockOrderData.Setup(o => o.GetOrderItemsByOrderId(It.IsAny<int>())).ReturnsAsync(stubOrderItemsData);
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductId(It.IsAny<int>())).ReturnsAsync(stubProductData);

            // ACT
            var result = await SystemUnderTest.GetOrderByOrderNumber(stubOrderNumber);

            // ASSERT
            result.ShouldBeOfType<OrderOutputModel>();
            result.OrderNumber.ShouldBe(stubOrderNumber);
            result.Id.ShouldBe(stubOrderData.Id);
        }

        [Test]
        public async Task MarkOrderItemComplete_OrderItemDoesNotExist_ReturnsSuccessFalseAndErrorMessage()
        {
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => null);

            var result = await SystemUnderTest.MarkOrderItemComplete(stubOrderNumber, stubProductCode);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.OrderItemDoesNotExist);
        }

        [Test]
        public async Task MarkOrderItemComplete_OrderItemExistsAndIsNotCompleted_ReturnsSuccessTrue()
        {
            var stubOrderItemData = StubsHelper.GenerateStubOrderItemData(false);
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(stubOrderItemData);

            var result = await SystemUnderTest.MarkOrderItemComplete(stubOrderNumber, stubProductCode);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeTrue();
            result.Error.ShouldBeNullOrEmpty();
        }

        [Test]
        public async Task MarkOrderItemComplete_OrderItemIsCompleted_ReturnsSuccessFalseAndErrorMessage()
        {
            var stubOrderItemData = StubsHelper.GenerateStubOrderItemData(true);
            var mockOrderData = AutoMock.Mock<IOrderData>().Setup(o => o.GetOrderItemByOrderNumberAndProductCode(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(stubOrderItemData);

            var result = await SystemUnderTest.MarkOrderItemComplete(stubOrderNumber, stubProductCode);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<OrderResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.NoChangeInData);
        }
    }
}
