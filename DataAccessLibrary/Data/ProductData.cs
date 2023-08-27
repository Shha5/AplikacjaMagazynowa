using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;
using DataAccessLibrary.SqlAccess.Interfaces;


namespace DataAccessLibrary.Data
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _dbAccess;
        public ProductData(ISqlDataAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Task InsertProduct(ProductDataModel product) =>
            _dbAccess.SaveData("sp_Product_Insert", new { product.ProductCode, product.ProductName, product.QuantityInStock });

        public Task InsertProductShipment(ShipmentDataModel shipment) =>
            _dbAccess.SaveData("sp_Shipment_InsertProductShipment", new { shipment.ProductCode, shipment.Quantity });

        public async Task<IEnumerable<ProductDataModel>> GetAllProducts()
        {
            var result = await _dbAccess.LoadData<ProductDataModel, dynamic>("sp_Product_GetAll", null);
            return result;
        }

        public async Task<ProductAvailabilityDataModel> GetProductDetailsByProductCode(string productCode)
        {
            var result = await _dbAccess.LoadData<ProductAvailabilityDataModel, dynamic>("sp_Product_GetProductDetailsByProductCode", new { productCode });
            return result.FirstOrDefault();
        }
    }
}
