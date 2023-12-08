using TestTime.Dto;
using TestTime.Models;

namespace TestTime.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> Get(string id);
        Task Add(string userId, string userName, ProductDto productDto);
        Task Update(string userId, string userName, string id, ProductDto productDto);
        Task Delete(string userId, string userName, string id);
        Task<double> CalculateTotalPrice(int quantity, double price);
        Task<RoleAndProductDto> RetrieveDto(string userId, string userName, string role);
    }
}
