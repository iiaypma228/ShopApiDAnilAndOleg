using System.Runtime.CompilerServices;
using Server.API.Models;

namespace Server.API.Services.Interfaces;

public interface IOrderService  : ICRUDService<Order>
{
    IList<Order> GetOrderByShopId(int shopId);
    List<OrderProduct> ReadProduct(string orderId);
    IList<Order> GetAllOrder();
    Shop ReadShopById(int id);
    void MoveOrder(string id);

    void UnMoveOrder(string id);
}