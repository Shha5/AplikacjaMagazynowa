using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaMagazynowaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public ProductController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Dodawanie do bazy danych nowego produktu o unikalnym kodzie.
        /// </summary>
        /// <remarks>
        /// Jeżeli produkt o danym kodzie istnieje już w bazie produkt nie zostanie dodany.
        /// Data dodania produktu przypisywana jest przez aplikację.
        /// </remarks>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("nowy-produkt")]
        public async Task<IActionResult> CreateNewProduct(ProductInputModel product)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            var result = await _inventoryService.CreateNewProduct(product);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Służy do zwiększania dostępnej ilości towaru, który został wcześniej dodany do bazy produktów.
        /// </summary>
        /// <param name="shipment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("nowa-dostawa")]
        public async Task<IActionResult> CreateNewProductShipment(ShipmentInputModel shipment)
        {
            if (ModelState.IsValid != true) 
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            var result = await _inventoryService.CreateNewProductShipment(shipment);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Przedstawia dostępne w bazie produkty wraz z dostępną ilością.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("wszystkie-produkty")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _inventoryService.GetAllProducts();
            if (products == null)
            {
                return StatusCode(500);
            }
            if (products.Count == 0 ) 
            {
                return NoContent();
            }
            return Ok(products);
        }
    }
}
