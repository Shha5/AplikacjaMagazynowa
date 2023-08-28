using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AplikacjaMagazynowaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("nowe-zamowienie")]
        public async Task<IActionResult> CreateNewOrder(OrderInputModel order)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest("Dane są niekompletne");
            }
            var result = await _orderService.SaveOrder(order);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("usun-pozycje-z-zamowienia")]
        public async Task<IActionResult> DeleteOrderItem(string orderNumber, string productCode)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            var result = await _orderService.DeleteOrderItem(orderNumber, productCode);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        [HttpPost]
        [Route("edytuj-pozycje-zamowienia")]
        public async Task<IActionResult> EditOrderItem(EditOrderItemInputModel orderEdit)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            if (orderEdit.NewQuantity <= 0)
            {
                return BadRequest(ErrorMessages.BadQuantity);
            }
            var result = await _orderService.EditOrderItem(orderEdit);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        [HttpGet]
        [Route("szczegoly-zamowienia")]
        public async Task<IActionResult> GetOrderByOrderNumber(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            var result = await _orderService.GetOrderByOrderNumber(orderNumber);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);  
        }

        [HttpPost]
        [Route("oznacz-pozycje-zrealizowana")]
        public async Task<IActionResult> MarkOrderItemComplete(string orderNumber, string productCode)
        {
            if (string.IsNullOrEmpty(orderNumber) || string.IsNullOrEmpty(productCode)) 
            {
                return BadRequest(ErrorMessages.DataIncomplete);
            }
            var result = await _orderService.MarkOrderItemComplete(orderNumber, productCode);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
    }
}
