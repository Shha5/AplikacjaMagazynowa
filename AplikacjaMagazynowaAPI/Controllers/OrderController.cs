using AplikacjaMagazynowaAPI.Constants;
using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Tworzenie nowego zamówienia o nieograniczonej liczbie produktów, które zawiera wyłącznie produkty istniejące w bazie.
        /// </summary>
        /// <remarks>
        /// Unikalny numer zamówienia oraz sygnatura są przypisywane przez aplikację.
        /// </remarks>
        /// <param name="order">Model zawierający listę zamawianych produktów.
        /// Liczba produktów jest dowolna.
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("nowe-zamowienie")]
        public async Task<IActionResult> CreateNewOrder(OrderInputModel order)
        {
            if (ModelState.IsValid != true)
            {
                return BadRequest("Dane są niekompletne");
            }
            var result = await _orderService.CreateOrder(order);
            if (result.Success != true)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        /// <summary>
        /// Usuwanie pozycji z zamówienia, o ile produkt o podanym kodzie powiązany z podanym numerem zamówienia znajduje się w bazie i pozycja nie została zrealizowana.
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("usun-pozycje-z-zamowienia")]
        public async Task<IActionResult> DeleteOrderItem(string orderNumber, string productCode)
        {
            if (string.IsNullOrWhiteSpace(orderNumber) || string.IsNullOrWhiteSpace(productCode))
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

        /// <summary>
        /// Edytowanie pozycji zamówienia o ile produkt o podanym kodzie powiązany z podanym numerem zamówienia znajduje się w bazie i pozycja nie została zrealizowana.
        /// </summary>
        /// <remarks>
        /// Edycji za pośrednictwem tego endpointu podlega jedynie ilość zamawianego produktu, która nie może być zerem.
        /// W celu usunięcia produktu z zamówienia, należy użyć <c>"usun-pozycje-z-zamowienia"</c>.
        /// </remarks>
        /// <param name="orderEdit"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Przedstawia szczegóły zamówienia o podanym numerze.
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("szczegoly-zamowienia")]
        public async Task<IActionResult> GetOrderByOrderNumber(string orderNumber)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
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

        /// <summary>
        /// Służy do oznaczania pozycji zamówienia jako zrealizowanej.
        /// </summary>
        /// <remarks>
        /// Oznaczenie pozycji zamówienia jako zrealizowanej uniemożliwi jej edycję oraz usunięcie.
        /// </remarks>
        /// <param name="orderNumber"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("oznacz-pozycje-zrealizowana")]
        public async Task<IActionResult> MarkOrderItemComplete(string orderNumber, string productCode)
        {
            if (string.IsNullOrWhiteSpace(orderNumber) || string.IsNullOrWhiteSpace(productCode)) 
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
