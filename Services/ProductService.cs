using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestTime.Data;
using TestTime.Dto;
using TestTime.Models;
using TestTime.Repositories;

namespace TestTime.Services
{
    public class ProductService : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;


        public ProductService(AppDbContext appDbContext, IConfiguration configuration, UserManager<User> userManager)
        {
            _context = appDbContext;
            _configuration = configuration;
            _userManager = userManager;
            
        }
        public async Task<List<Product>> GetAllProducts() {
            return await _context.Products.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Product> GetSingleProduct(int id) {
            var task = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (task is null)
            {
                throw new BadHttpRequestException("Task not found");
            }
            return task ?? throw new BadHttpRequestException("Task not found");
        }


        public async Task Add(string userId, string userName, ProductDto productDto)
        {
            var product = new Product
            {
                Title = productDto.Title,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                TotalPrice = await CalculateTotalPrice(productDto.Quantity, productDto.Price)
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync(userId, userName);
        }


        public async Task Update(string userId, string userName, int id, ProductDto productDto)
        {
            var vat = _configuration["Vat"]!;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is not null)
            {
                product.Title = productDto.Title;
                product.Price = productDto.Price;
                product.Quantity = productDto.Quantity;
                product.TotalPrice = (productDto.Quantity * productDto.Price) * (1 + Convert.ToDouble(vat));
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync(userId, userName);
            }
            else
            {
                throw new BadHttpRequestException("Product not found");
            }
        }

        public async Task Delete(int id, string userId, string userName)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(userId, userName);
            }
        }

        public Task<double> CalculateTotalPrice(int quantity, double price)
        {
            var vat = _configuration["Vat"]!;

            return Task.FromResult((quantity * price) * (1 + Convert.ToDouble(vat)));
        }

    }
}
