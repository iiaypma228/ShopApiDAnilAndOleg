using Server.API.Models;

namespace Server.API.Database.Interfaces;

public interface IOrderProductRepository : IRepository<OrderProduct>, IDisposable
{
    
}