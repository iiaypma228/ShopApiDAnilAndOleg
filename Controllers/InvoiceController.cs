using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.API.Services.Interfaces;

namespace Server.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoiceController : Controller
{
    private readonly IInvoiceService _service = null;
    private readonly IInvoiceProductService _productService = null;
    
    public InvoiceController(IServiceContainer _container)
    {
        this._service = _container.Resolve<IInvoiceService>();
        this._productService = _container.Resolve<IInvoiceProductService>();
    }

    [HttpGet("getinvoices")]
    public IActionResult GetInvoices(int shopId)
    {
        if (shopId == -5)
        {
            var result = this._service.ReadAllInvoice();
            return Ok(result);
        }
        else
        {
            var result = this._service.ReadInvoiceByShopId(shopId);
            return Ok(result);
        }
    }


    [HttpGet("getinvoice")]
    public IActionResult GetInvoice(string invoiceId)
    {
        var invoice = this._service.Read(invoiceId);

        invoice.ShopIn = this._service.ReadShop(invoice.ShopInId.Value);
        invoice.ShopOut = this._service.ReadShop(invoice.ShopOutId.Value);

        invoice.Employee = this._service.ReadEmployee(invoice.EmployeeId.Value);

        invoice.InvoiceProducts = this._service.ReadProduct(invoiceId);

        return Ok(invoice);

    }
    
    
    
}