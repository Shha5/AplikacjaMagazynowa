using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data.Interfaces
{
    public interface IProductData
    {
        Task InsertProduct(ProductDataModel product);
        Task<IEnumerable<ProductDataModel>> GetAllProducts();
        Task InsertProductShipment(ShipmentDataModel shipment);
        Task<ProductAvailabilityDataModel> GetProductDetailsByProductCode(string productCode);
    }
}