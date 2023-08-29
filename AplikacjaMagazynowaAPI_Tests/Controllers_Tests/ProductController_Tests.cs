using AplikacjaMagazynowaAPI.Controllers;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaMagazynowaAPI_Tests.Controllers_Tests
{
    [TestFixture]
    internal class ProductController_Tests : TestBase<ProductController>
    {
        private const string sampleError = "Sample Error";
        private readonly ProductInputModel validProductInputStub = StubsHelper.GenerateStubProductInput();
        private readonly ShipmentInputModel validShipmentInputStub = StubsHelper.GenerateStubShipmentInput();
        private readonly InventoryResultModel stubUnsuccessfullInventoryResult = new InventoryResultModel() { Success = false, Error = sampleError };
        private readonly InventoryResultModel stubSuccessfullInventoryResult = new InventoryResultModel() { Success = true };


        [Test]
        public async Task CreateNewProduct_InventoryServiceReturnsFalse_ReturnsBadRequest()
        {
            var mockInventoryService = AutoMock.Mock<IInventoryService>().Setup(x => x.CreateNewProduct(It.IsAny<ProductInputModel>()))
                .ReturnsAsync(stubUnsuccessfullInventoryResult);

            var result = await SystemUnderTest.CreateNewProduct(validProductInputStub);

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task CreateNewProduct_InventoryServiceReturnsTrue_ReturnsOk()
        {
            var mockInventoryService = AutoMock.Mock<IInventoryService>().Setup(x => x.CreateNewProduct(It.IsAny<ProductInputModel>()))
                .ReturnsAsync(stubSuccessfullInventoryResult);

            var result = await SystemUnderTest.CreateNewProduct(validProductInputStub);

            result.ShouldBeOfType<OkResult>();
        }

        [Test]
        public async Task CreateNewProductShipment_InventoryServiceReturnsFalse_ReturnsBadRequest()
        {
            var mochInventoryService = AutoMock.Mock<IInventoryService>().Setup(x => x.CreateNewProductShipment(It.IsAny<ShipmentInputModel>()))
                .ReturnsAsync(stubUnsuccessfullInventoryResult);

            var result = await SystemUnderTest.CreateNewProductShipment(validShipmentInputStub);

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task CreateNewProductShipment_InventoryServiceReturnsTrue_ReturnsOk()
        {
            var mockInventoryService = AutoMock.Mock<IInventoryService>().Setup(x => x.CreateNewProductShipment(It.IsAny<ShipmentInputModel>()))
                .ReturnsAsync(stubSuccessfullInventoryResult);

            var result = await SystemUnderTest.CreateNewProductShipment(validShipmentInputStub);

            result.ShouldBeOfType<OkResult>();
        }

        [Test]
        public async Task GetAllProducts_ProductsAreNull_ReturnsStatusCode()
        {
            var mockInventoryService = AutoMock.Mock<IInventoryService>().Setup(x => x.GetAllProducts())
               .ReturnsAsync(() => null);

            var result = await SystemUnderTest.GetAllProducts();

            result.ShouldBeOfType<StatusCodeResult>();
        }

        [Test]
        public async Task GetAllProducts_ProductsAreEmpty_ReturnsNoContent()
        {
            var mockInventoryService = AutoMock.Mock<IInventoryService>().Setup(x => x.GetAllProducts())
               .ReturnsAsync(new List<ProductOutputModel>());

            var result = await SystemUnderTest.GetAllProducts();

            result.ShouldBeOfType<NoContentResult>();
        }

        [Test]
        public async Task GetAllProducts_ProductsWereFound_ReturnsOk()
        {
            var validProductOutputStub = StubsHelper.GenerateValidStubProductOutputList();
            var mockInventoryService = AutoMock.Mock<IInventoryService>().Setup(x => x.GetAllProducts())
              .ReturnsAsync(validProductOutputStub);

            var result = await SystemUnderTest.GetAllProducts();

            result.ShouldBeOfType<OkObjectResult>();
        }
    }
}
