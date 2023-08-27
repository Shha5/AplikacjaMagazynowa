using AplikacjaMagazynowaAPI.Models.InputModels;
using AplikacjaMagazynowaAPI.Models.OutputModels;

namespace AplikacjaMagazynowaAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResultModel> SaveOrder(List<OrderItemInputModel> orderItems);
    }
}