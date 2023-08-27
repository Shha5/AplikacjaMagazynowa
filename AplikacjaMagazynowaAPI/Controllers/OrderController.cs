using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        //TWORZENIE ZAMÓWIEŃ NA TOWAR ISTNIEJĄCY W BAZIE
        [HttpPost]
        [Route("nowe-zamowienie")]
        public async Task<IActionResult> CreateNewOrder(List<OrderItemInputModel> orderDetails)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest("Dane są niekompletne");
            }
            var result = await _orderService.SaveOrder(orderDetails);
            if (result.Success != true)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }
        //EDYCJA POZYCJI ZAMÓWIENIA, KTÓRA NIE ZOSTAŁA ZREALIZOWANA
    }
}
