using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTime.Dto;
using TestTime.Models;
using TestTime.Repositories;
using TestTime.Services;
using X.PagedList;

namespace TestTime.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
    public class ProductController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository, UserManager<User> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            var tasks = await _productRepository.GetAllProducts();

            var pageNumber = page ?? 1;
            var paginatedTasks = tasks.ToPagedList(pageNumber, pageSize);

            return View(paginatedTasks);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetSingleProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = new ProductDto
            {
                Title = product.Title,
                Quantity = product.Quantity,
                Price = product.Price,
                //TotalPrice = product.TotalPrice,
            };
            return View(productDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync();
            try
            {
                var product = await _productRepository.GetSingleProduct(id);

                if (product == null)
                {
                    return NotFound();
                }

                await _productRepository.Update(user.Id, user.UserName,  id,  productDto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return View(productDto);
            }
        }





        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _productRepository.GetSingleProduct(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _productRepository.Delete(id, userId!, username!);

            return RedirectToAction("Index");
        }



        [Authorize(Roles = "ADMIN")]
        [HttpGet] // Remove this line if you have it
        public async Task<ViewResult> Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Add(string userId, string userName, ProductDto productDto)
        {

           
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            var userid = user!.Id;

            await _productRepository.Add(userid,  username, productDto);


            return RedirectToAction("Index");

        }
    }
}
