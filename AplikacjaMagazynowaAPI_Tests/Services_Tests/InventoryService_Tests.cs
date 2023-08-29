using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Services;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;

namespace AplikacjaMagazynowaAPI_Tests.Services_Tests
{
    [TestFixture]
    internal class InventoryService_Tests : TestBase<InventoryService>
    {
        private readonly ProductInputModel validStubProductInput = StubsHelper.GenerateStubProductInput();
        private readonly ShipmentInputModel validStubShipmentInput = StubsHelper.GenerateStubShipmentInput();
        private readonly ProductAvailabilityDataModel stubProductExistsResult = StubsHelper.GenerateStubAvailabilityData();

        [Test]
        public async Task CreateNewProduct_ProductDoesNotExistQuantityNotNegative_ReturnsSuccessTrue()
        {
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var result = await SystemUnderTest.CreateNewProduct(validStubProductInput);

            result.ShouldBeOfType<InventoryResultModel>();
            result.Success.ShouldBeTrue();
            result.Error.ShouldBeNullOrEmpty();
        }

        [Test]
        public async Task CreateNewProduct_ProductExists_ReturnsSuccessFalseAndErrorMessage()
        {
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>()))
                .ReturnsAsync(stubProductExistsResult);

            var result = await SystemUnderTest.CreateNewProduct(validStubProductInput);

            result.ShouldBeOfType<InventoryResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.ProductExistsInDb);
        }

        [Test]
        public async Task CreateNewProduct_QuantityLessThanZero_ReturnsSuccessFalseAndErrorMessage()
        {
            var invalidStubProductInput = StubsHelper.GenerateStubProductInput(false);

            var result = await SystemUnderTest.CreateNewProduct(invalidStubProductInput);

            result.ShouldBeOfType<InventoryResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.BadQuantity);
        }

        [Test]
        public async Task CreateNewProductShipment_QuantityLessThanOrZero_ReturnsSuccessFalseAndErrorMessage()
        {
            var invalidStubShipmentInput = StubsHelper.GenerateStubShipmentInput(false);

            var result = await SystemUnderTest.CreateNewProductShipment(invalidStubShipmentInput);

            result.ShouldBeOfType<InventoryResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.BadQuantity);
        }

        [Test]
        public async Task CreateNewProductShipment_ProductDoesNotExists_ReturnsSuccessFalseAndErrorMessage()
        {
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var result = await SystemUnderTest.CreateNewProductShipment(validStubShipmentInput);

            result.ShouldBeOfType<InventoryResultModel>();
            result.Success.ShouldBeFalse();
            result.Error.ShouldNotBeNullOrEmpty();
            result.Error.ShouldContain(ErrorMessages.ProductUnavailable);
        }

        [Test]
        public async Task CreateNewProductShipment_ProductExistsQuantityPositive_ReturnsSuccessFalseAndErrorMessage()
        {
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetProductDetailsByProductCode(It.IsAny<string>()))
                .ReturnsAsync(stubProductExistsResult);

            var result = await SystemUnderTest.CreateNewProductShipment(validStubShipmentInput);

            result.ShouldBeOfType<InventoryResultModel>();
            result.Success.ShouldBeTrue();
            result.Error.ShouldBeNullOrEmpty();
        }

        [Test]
        public async Task GetAllProducts_ProductDataReturnsNull_ReturnsEmptyListOfProductOutputModel()
        {
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetAllProducts())
                .ReturnsAsync(() => null);

            var result = await SystemUnderTest.GetAllProducts();

            result.ShouldBeOfType<List<ProductOutputModel>>();
            result.Count.ShouldBe(0);
        }

        [Test]
        public async Task GetAllProducts_ProductDataReturnsData_ReturnsListOfProductOutputModel()
        {
            var stubValidResult = StubsHelper.GenerateStubValidProductDataIEnum();
            var mockProductData = AutoMock.Mock<IProductData>().Setup(p => p.GetAllProducts())
                .ReturnsAsync(stubValidResult);

            var result = await SystemUnderTest.GetAllProducts();

            result.ShouldBeOfType<List<ProductOutputModel>>();
            result.Count.ShouldNotBe(0);
        }
    }
}
