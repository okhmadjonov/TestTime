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


<<<<<<< HEAD

        [Authorize(Roles = "ADMIN")]
        [HttpGet] // Remove this line if you have it
        public async Task<ViewResult> Add()
=======
            await _productService.Update(userId, userName, id, productDto);
            var roleProductDto = await _productService.RetrieveDto(userId, userName, role);
            return View("Update", roleProductDto);
        }

        // [HttpPost]
        // [Authorize(Roles = "ADMIN")]
        // public async Task<IActionResult> DeleteAsync(string userId, string userName, string role, string id)
        // {
        //     if (!ModelState.IsValid) return View("Index");

        //     await _productService.Delete(userId, userName, id);
        //     var roleProductDto = await _productService.RetrieveDto(userId, userName, role);
        //     return View("Delete", roleProductDto);
        // }

             // Delete task
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            var task = await _productService.Get(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _productService.Delete(id, userId!, username!);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet] // Remove this line if you have it
        public async Task<ViewResult> Create()
>>>>>>> 8caa2bbb374c7ecd022d6eeff06c1775f178a309
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Add(string userId, string userName, ProductDto productDto)
        {
<<<<<<< HEAD

           
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            var userid = user!.Id;

            await _productRepository.Add(userid,  username, productDto);

=======
       
            await _productService.Add(userId, userName, productDto);
            var roleProductDto = await _productService.RetrieveDto(userId, userName, role);
>>>>>>> 8caa2bbb374c7ecd022d6eeff06c1775f178a309

            return RedirectToAction("Index");

        }
    }
}
