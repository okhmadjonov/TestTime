using TestTime.Dto;
using TestTime.Models;

namespace TestTime.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetSingleProduct(int id);
        Task Add(string userId, string userName, ProductDto productDto);
        Task Update(string userId, string userName, int id, ProductDto productDto);
        Task Delete(int id, string userId, string username);
        Task<double> CalculateTotalPrice(int quantity, double price);
       
    }
}
