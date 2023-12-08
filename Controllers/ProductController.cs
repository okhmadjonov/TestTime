using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTime.Dto;
using TestTime.Models;
using TestTime.Repositories;
using TestTime.Services;

namespace TestTime.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productService;
        private readonly UserManager<User> _userManager;

        public ProductController(IProductRepository productService, UserManager<User> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId!);
            var rolesAsync = await _userManager.GetRolesAsync(user);
            string role = rolesAsync[0];
            string id = userId!;
            string userName = user.UserName!;

            var roleProductDto = await _productService.RetrieveDto(id, userName, role);
            return View("Index", roleProductDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateAsync(string userId, string role, string userName, string id, ProductDto productDto)
        {
            if (!ModelState.IsValid) return View("Index");

            await _productService.Update(userId, userName, id, productDto);
            var roleProductDto = await _productService.RetrieveDto(userId, userName, role);
            return View("Update", roleProductDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteAsync(string userId, string userName, string role, string id)
        {
            if (!ModelState.IsValid) return View("Index");

            await _productService.Delete(userId, userName, id);
            var roleProductDto = await _productService.RetrieveDto(userId, userName, role);
            return View("Delete", roleProductDto);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet] // Remove this line if you have it
        public async Task<ViewResult> Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(string userId, string userName, string role, ProductDto productDto)
        {
       
            await _productService.Add(userId, userName, productDto);
            var roleProductDto = await _productService.RetrieveDto(userId, userName, role);

            return RedirectToAction("Index");

        }
    }
}
