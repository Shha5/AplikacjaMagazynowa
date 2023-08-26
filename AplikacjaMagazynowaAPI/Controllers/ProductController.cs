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

            int result = await _productData.CreateProduct(productData);
            if (result == -1)
            {
                return BadRequest("Produkt istnieje w bazie. Dodaj dostawę, żeby zwiększyć ilość towaru w magazynie.");
            }
            if (result != 0 && result != -1)
            {
                return StatusCode(500, "Wystąpił nieoczekiwany błąd. Ponów próbę później.");
            }
           
            return Ok();
        }

        //PRZEGLĄD WSZYSTKICH PRODUKTÓW DOSTĘPNYCH W BAZIE
        
    }
}
