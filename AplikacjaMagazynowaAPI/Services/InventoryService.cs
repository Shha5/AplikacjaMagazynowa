﻿using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;

namespace AplikacjaMagazynowaAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductData _productData;

        public InventoryService(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<InventoryResultModel> CreateNewProduct(ProductInputModel product)
        {
            if (product.QuantityInStock < 0)
            {
                return GenerateUnsuccessfulInventoryResult(ErrorMessages.BadQuantity);
            }
            ProductDataModel productData = new ProductDataModel()
            {
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                QuantityInStock = product.QuantityInStock
            };
            if (await CheckIfProductExists(productData.ProductCode) == true)
            {
                return GenerateUnsuccessfulInventoryResult(ErrorMessages.ProductExistsInDb);
            }
            await _productData.InsertProduct(productData);
            return GenerateSuccessfulInventoryResult();
        }

        public async Task<InventoryResultModel> CreateNewProductShipment(ShipmentInputModel shipment)
        {
            if (shipment.Quantity <= 0)
            {
                return GenerateUnsuccessfulInventoryResult(ErrorMessages.BadQuantity);
            }
            if (await CheckIfProductExists(shipment.ProductCode) == false)
            {
                return GenerateUnsuccessfulInventoryResult(ErrorMessages.ProductUnavailable);
            }
            ShipmentDataModel shipmentData = new ShipmentDataModel()
            {
                ProductCode = shipment.ProductCode,
                Quantity = shipment.Quantity,
            };
            await _productData.InsertProductShipment(shipmentData);
            return GenerateSuccessfulInventoryResult();
        }

        public async Task<List<ProductOutputModel>> GetAllProducts()
        {
            List<ProductOutputModel> products = new List<ProductOutputModel>();
            var result = await _productData.GetAllProducts();
            if (result != null && result.Count() > 0)
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

        private InventoryResultModel GenerateSuccessfulInventoryResult()
        {
            return new InventoryResultModel()
            {
                Success = true
            };
        }

        private InventoryResultModel GenerateUnsuccessfulInventoryResult(string ErrorMessage)
        {
            return new InventoryResultModel()
            {
                Success = false,
                Error = ErrorMessage
            };
        }
    }
}
