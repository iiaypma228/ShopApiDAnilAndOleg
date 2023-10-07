using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService service = null;
    private readonly ICategoryProductService categoryProductService = null;
    
    public ProductController(IServiceContainer container)
    {
        service = container.Resolve<IProductService>();
        categoryProductService= container.Resolve<ICategoryProductService>();
    }

    [HttpGet("getallproduct")]
    public IActionResult GetAllProduct()
    {
        var result = this.service.Read().ToList();

        return Ok(result);
    }

    [HttpPost("addproduct")]

    public IActionResult AddProduct(int shopId, Product product)
    {
        if (shopId == -5)
        {
            this.service.Save(product);
            return Ok();
        }
        else
        {
            return BadRequest("Не має прав на додавання товару!");
        }
    }

    [HttpPost("createcategoryproduct")]

    public IActionResult CreateCategoryProduct(CategoryProduct category)
    {
        this.categoryProductService.Save(category);
        return Ok();
    }
}