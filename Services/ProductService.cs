using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IConfiguration _configuration;


        public ProductService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _context = appDbContext;
            _configuration = configuration;
            
        }

        //Get all products

        public async Task<List<Product>> GetAll() { 
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Get(string id) { 
            var product = await _context.Products.FirstOrDefaultAsync(p=> p.Id==id);
              return product ?? throw new  BadHttpRequestException("Product not found.");
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


        public async Task Update(string userId, string userName, string id, ProductDto productDto)
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

        public async Task Delete(string userId, string userName, string id)
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

        public async Task<RoleAndProductDto> RetrieveDto(string userId, string userName, string role)
        {
            RoleAndProductDto roleProductDto = new RoleAndProductDto
            {
                Id = userId,
                Name = userName,
                Role = role,
                Products = await GetAll()
            };

            roleProductDto.Products = roleProductDto.Products.OrderBy(p => p.Id).ToList();

            return roleProductDto;
        }
    }
}
