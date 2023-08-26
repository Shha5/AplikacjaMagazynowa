using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data.Interfaces
{
    public interface IProductData
    {
        Task<int> CreateProduct(ProductDataModel product);
    }
}