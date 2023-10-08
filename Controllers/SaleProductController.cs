using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SaleProductController : Controller
{
    private readonly ISaleProductService _service = null;

    public SaleProductController(IServiceContainer container)
    {
        this._service = container.Resolve<ISaleProductService>();
    }

    [HttpGet("getsalesproduct")]
    public IActionResult GetSalesProduct(int shopId)
    {
        if (shopId == -5)
        {
            var item = this._service.Read().ToList();
            return Ok(item);
        }
        else
        {
            var item = this._service.Read(i => i.ShopId == shopId).ToList();
            return Ok(item);
        }
    }

    [HttpPost("createsaleproduct")]
    public IActionResult CreateSaleProduct(List<SaleProduct> saleProduct)
    {
        try
        {
            this._service.SaveSale(saleProduct);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest($"Виникла помилка при зберіганні продажу: {e.Message}");
        }
    }

}