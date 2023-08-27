using AplikacjaMagazynowaAPI.Models;
using DataAccessLibrary.Data.Interfaces;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaMagazynowaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        // DODAWANIE NOWEGO TOWARU
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest("Dane są niekompletne.");
            }
            ProductDataModel productData = new ProductDataModel()
            {
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                QuantityInStock = product.QuantityInStock,
            };

            await _productData.CreateProduct(productData);
            return Ok();
        }

        // ZWIĘKSZENIE LICZBY DOSTĘPNYCH SZTUK TOWARU ISTNIEJĄCEGO W BAZIE
        [HttpPost]
        [Route("AddProductShipment")]
        public async Task<IActionResult> AddProductShipment(ShipmentModel shipment)
        {
            if (ModelState.IsValid != true) 
            {
                return BadRequest("Dane są niekompletne.");
            }
            ShipmentDataModel shipmentData = new ShipmentDataModel()
            {
                ProductCode = shipment.ProductCode,
                Quantity = shipment.Quantity
            };

            await _productData.AddProductShipment(shipmentData);
            return Ok();
        }

        // PRZEGLĄD WSZYSTKICH PRODUKTÓW DOSTĘPNYCH W BAZIE
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            var result = await _productData.GetAllProducts();
            if (result.Count() == 0)
            {
                return StatusCode(500);
            }
            foreach (var product in result)
            {
                products.Add(new ProductModel
                {
                    Id = product.Id,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    QuantityInStock = product.QuantityInStock,
                    CreatedDate = product.CreatedDate,
                    LastModifiedDate = product.LastModifiedDate
                });
            }
            return Ok(products);
        }
    }
}
