using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;

namespace AplikacjaMagazynowaAPI.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<InventoryResultModel> SaveNewProduct(ProductInputModel product);
        Task<InventoryResultModel> SaveNewProductShipment(ShipmentInputModel shipment);
        Task<List<ProductOutputModel>> GetAllProducts();
    }
}