using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTime.Dto;
using TestTime.Models;
using TestTime.Repositories;

namespace TestTime.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "ADMIN")]
public class ProductApiController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly UserManager<User> _userManager;
    public ProductApiController(IProductRepository productRepository, UserManager<User> userManager)
    {
        _productRepository = productRepository;
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> Index() => Ok(await _productRepository.GetAllProducts());

    [HttpGet("id")]
    public async Task<IActionResult> Index(int id) => Ok(await _productRepository.GetSingleProduct(id));

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto product)
    {
      
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.GetUserAsync(User);
        var username = user!.UserName;
        await _productRepository.Add( userId!, username!, product);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Edit(int id, ProductDto productDto)
    {
      
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.GetUserAsync(User);
        var username = user!.UserName;
        var email = user!.Email;
        await _productRepository.Update( userId!, username!,id, productDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.GetUserAsync(User);
        var username = user!.UserName;
        await _productRepository.Delete(id, userId!, username!);
        return Ok();
    }
}
