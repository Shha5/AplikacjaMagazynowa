using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data.Interfaces
{
    public interface IProductData
    {
        Task CreateProduct(ProductDataModel product);
    }
}