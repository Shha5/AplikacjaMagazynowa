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

        //DODAWANIE NOWEGO TOWARU
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

        //PRZEGLĄD WSZYSTKICH PRODUKTÓW DOSTĘPNYCH W BAZIE
        
    }
}
