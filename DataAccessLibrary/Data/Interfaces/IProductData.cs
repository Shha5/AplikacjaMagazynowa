using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data.Interfaces
{
    public interface IProductData
    {
        Task<IEnumerable<ProductDataModel>> GetAllProducts();
        Task<ProductAvailabilityDataModel> GetProductDetailsByProductCode(string productCode);
        Task<ProductDataModel> GetProductDetailsByProductId(int productId);
        Task InsertProduct(ProductDataModel product);
        Task InsertProductShipment(ShipmentDataModel shipment);
    }
}
