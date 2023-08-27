using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data.Interfaces
{
    public interface IProductData
    {
        Task CreateProduct(ProductDataModel product);
        Task<IEnumerable<ProductDataModel>> GetAllProducts();
        Task AddProductShipment(ShipmentDataModel shipment);
    }
}