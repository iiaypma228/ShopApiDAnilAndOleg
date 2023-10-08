using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.API.Services.Interfaces;

namespace Server.API.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductRestController : Controller
{
    private readonly IProductRestService _service = null;

    public ProductRestController(IServiceContainer container)
    {
        this._service = container.Resolve<IProductRestService>();
    }

    [HttpGet("getproductrests")]
    public IActionResult GetProductRests(int shopId)
    {
        if (shopId == -5)
        {
            var items = this._service.Read();
            return Ok(items);
        }
        else
        {
            var items = this._service.Read(i => i.ShopId == shopId);
            return Ok(items);
        }
    }
    
    
}