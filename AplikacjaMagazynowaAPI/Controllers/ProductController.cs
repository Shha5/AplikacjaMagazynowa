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

        [HttpPost]
        [Route("nowy-produkt")]
        public async Task<IActionResult> CreateNewProduct(ProductInputModel product)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            var result = await _inventoryService.SaveNewProduct(product);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        [HttpPost]
        [Route("nowa-dostawa")]
        public async Task<IActionResult> CreateNewProductShipment(ShipmentInputModel shipment)
        {
            if (ModelState.IsValid != true) 
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            var result = await _inventoryService.SaveNewProductShipment(shipment);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

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
