using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;

namespace AplikacjaMagazynowaAPI.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<List<ProductOutputModel>> GetAllProducts();
        Task<InventoryResultModel> CreateNewProduct(ProductInputModel product);
        Task<InventoryResultModel> CreateNewProductShipment(ShipmentInputModel shipment);
    }
}