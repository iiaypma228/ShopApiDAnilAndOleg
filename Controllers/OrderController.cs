using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Server.API.Models;
using Server.API.Models.Dtos;
using Server.API.Services.Interfaces;

namespace Server.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService = null;
    private readonly IOrderProductService _orderProductService = null;
    
    public OrderController(IServiceContainer container)
    {
        _orderService = container.Resolve<IOrderService>();
        _orderProductService = container.Resolve<IOrderProductService>();
    }
    
    
    [HttpPost("createorder")]
    public IActionResult CreateOrder(Order? order)
    {
        try
        {
            this._orderService.Save(order);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest("Виникла помилка при зберіганні документу на сервері:" + ex.Message);
        }
    }

    [HttpPost("deleteorder")]
    public IActionResult DeleteOrder(string orderId)
    {
        var order = this._orderService.Read(orderId);
        order.OrderProducts = this._orderService.ReadProduct(orderId);
        switch (order.Status)
        {
            case DocumentStatus.Draft:
                this._orderService.Delete(order);
                return Ok();
            default:
                return BadRequest("На зараз цей документ видалити не можна, відмініть проведння та спробуйте ще раз!");
        }
        
    }

    [HttpPost("unmoveorder")]
    public IActionResult UnMoveOrder(int shopId, string orderId)
    {
        var order = this._orderService.Read(orderId);
        
        if (order.Status == DocumentStatus.Draft)
            return BadRequest("Відміна проведення не можлива, документ має статус Чернетка!");
        
        if (shopId == -5)
        {
            
            try
            {
                this._orderService.UnMoveOrder(orderId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest("Відмова модулю проведення:" + ex.Message);
            }
            
        }
        else
        {
             if (order.Status == DocumentStatus.Move)
                return BadRequest(
                    "Відміна проведення не можлива документ вже проведено на центральному офісі!");
             
            order.Status = DocumentStatus.Draft;
            this._orderService.Save(order);
            return Ok();
        }
    }
    
    
    [HttpPost("moveorder")]
    public IActionResult MoveOrder(int shopId, string orderId)
    {
        var order = this._orderService.Read(orderId);
        
        if (order.Status == DocumentStatus.Move)
            return BadRequest("Документ вже проведено!");
        
        if (shopId == -5)
        {
            if (order.Status == DocumentStatus.Draft && order.ShopId != -5)
                return BadRequest("Проведення не можливо, так як на магазині цей документ не було проведено!");

            try
            {
                this._orderService.MoveOrder(orderId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest("Відмова модулю проведення:" + ex.Message);
            }
            
        }
        else
        {
             if (order.Status == DocumentStatus.InProccess)
                return BadRequest(
                    "На зараз документу надано статус \"В обробці\", очікуйте подальших дій від Центрального Офісу!");

            order.Status = DocumentStatus.InProccess;
            this._orderService.Save(order);
            return Ok();
        }
    }
    
    [HttpGet("getorders")]
    public IActionResult GetOrders(int shopId)
    {
        if (shopId == -5)
        { 
            var result = this._orderService.GetAllOrder();

            var dto = new OrdersDto()
            {
                Orders = result,
                Shop = this._orderService.ReadShopById(shopId)
            };
        
            return Ok(dto);
        }
        else
        {
            var result = this._orderService.GetOrderByShopId(shopId);

            var dto = new OrdersDto()
            {
                Orders = result,
                Shop = this._orderService.ReadShopById(shopId)
            };
        
            return Ok(dto);
        }

    }

    [HttpGet("getorder")]
    public IActionResult GetOrder(string orderId)
    {
        var result = this._orderService.Read(orderId);

        result.Shop = this._orderService.ReadShopById(result.ShopId);
        
        result.OrderProducts = this._orderService.ReadProduct(orderId);

        return Ok(result);

    }
    
    
    [HttpGet("getnextordernumber")]
    public IActionResult GetNextOrderNumber()
    {
        string result = string.Empty;
        var orders = this._orderService.Read();
        
        
        
        if (orders.Any())
        {
            string[] parts =  orders.Max(n => n.Number).Split('-');
        
            if (parts.Length == 2 && int.TryParse(parts[1], out int number))
            {
                number++;
                result = parts[0] + "-" + number;
                
            }
            else
            {
                result = Guid.NewGuid().ToString();
            }  
        }
        else
            result = "ЗВ-1";
        
        return Ok(result);
    }
    
}