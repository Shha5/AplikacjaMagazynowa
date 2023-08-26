using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;
using DataAccessLibrary.SqlAccess;
using DataAccessLibrary.SqlAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _dbAccess;
        public ProductData(ISqlDataAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Task CreateProduct(ProductDataModel product) =>
             _dbAccess.SaveData("sp_Product_Create", new { product.ProductCode, product.ProductName, product.QuantityInStock });

        public async Task<IEnumerable<ProductDataModel>> GetAllProducts()
        {
            var result = await _dbAccess.LoadData<ProductDataModel, dynamic>("sp_Product_GetAll", null);
            return result;
        }

        public Task AddProductShipment(ShipmentDataModel shipment) =>
            _dbAccess.SaveData("sp_Shipment_Product_AddProductShipment", new { shipment.ProductCode, shipment.Quantity });
    }
}
