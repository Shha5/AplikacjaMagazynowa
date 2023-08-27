using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using DataAccessLibrary.Data;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;
using System.Security.Cryptography.Xml;

namespace AplikacjaMagazynowaAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductData _productData;

        public InventoryService(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<InventoryResultModel> SaveNewProduct(ProductInputModel product)
        {
            if (product.QuantityInStock < 0)
            {
                return new InventoryResultModel()
                {
                    Success = false,
                    Error = ErrorMessages.QuantityLessThanZero
                };
            }
            ProductDataModel productData = new ProductDataModel()
            {
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                QuantityInStock = product.QuantityInStock
            };
            if (await CheckIfProductExists(productData.ProductCode) == true)
            {
                return new InventoryResultModel()
                {
                    Success = false,
                    Error = ErrorMessages.ProductExistsInDb
                };
            }
            await _productData.InsertProduct(productData);
            return new InventoryResultModel()
            {
                Success = true
            };
        }

        public async Task<InventoryResultModel> SaveNewProductShipment(ShipmentInputModel shipment)
        {
            if (shipment.Quantity <= 0)
            {
                return new InventoryResultModel()
                {
                    Success = false,
                    Error = ErrorMessages.QuantityLessOrZero
                };
            }
            if (await CheckIfProductExists(shipment.ProductCode) == false)
            {
                return new InventoryResultModel()
                {
                    Success = false,
                    Error = ErrorMessages.ProductDoesNotExist
                };
            }
            ShipmentDataModel shipmentData = new ShipmentDataModel()
            {
                ProductCode = shipment.ProductCode,
                Quantity = shipment.Quantity,
            };
            await _productData.InsertProductShipment(shipmentData);
            return new InventoryResultModel()
            {
                Success = true
            };
        }

        public async Task<List<ProductOutputModel>> GetAllProducts()
        {
            List<ProductOutputModel> products = new List<ProductOutputModel>();
            var result = await _productData.GetAllProducts();
            if (result != null)
            {
                foreach (var product in result)
                {
                    products.Add(new ProductOutputModel
                    {
                        Id = product.Id,
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        QuantityInStock = product.QuantityInStock,
                        CreatedDate = product.CreatedDate,
                        LastModifiedDate = product.LastModifiedDate
                    });
                }
            }
            return products;
        }
       
        private async Task<bool> CheckIfProductExists(string productCode)
        {
            var productDetails = await _productData.GetProductDetailsByProductCode(productCode);
            if (productDetails != null)
            {
                return true;
            }
            return false;
        }
    }
}
