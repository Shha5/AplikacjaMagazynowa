using DataAccessLibrary.Constants;
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

        public async Task<IEnumerable<ProductDataModel>> GetAllProducts()
        {
            var result = await _dbAccess.LoadData<ProductDataModel, dynamic>(StoredProceduresNames.spProductGetAll, null);
            return result;
        }

        public async Task<ProductAvailabilityDataModel> GetProductDetailsByProductCode(string productCode)
        {
            var result = await _dbAccess.LoadData<ProductAvailabilityDataModel, dynamic>(StoredProceduresNames.spProductGetProductDetailsByProductCode, new { productCode });
            return result.FirstOrDefault();
        }

        public async Task<ProductDataModel> GetProductDetailsByProductId(int productId)
        {
            var result = await _dbAccess.LoadData<ProductDataModel, dynamic>(StoredProceduresNames.spProductGetProductDetailsById, new { productId });
            return result.FirstOrDefault();
        }

        public Task InsertProduct(ProductDataModel product) =>
          _dbAccess.SaveData(StoredProceduresNames.spProductInsert, new { product.ProductCode, product.ProductName, product.QuantityInStock });

        public Task InsertProductShipment(ShipmentDataModel shipment) =>
            _dbAccess.SaveData(StoredProceduresNames.spShipmentInsert, new { shipment.ProductCode, shipment.Quantity });
    }
}
