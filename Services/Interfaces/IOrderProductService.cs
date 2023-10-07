using Server.API.Models;

namespace Server.API.Services.Interfaces;

public interface IOrderProductService  : ICRUDService<OrderProduct>
{
    void SaveLink(string orderId, List<OrderProduct> items);

}