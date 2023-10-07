using Server.API.Models;

namespace Server.API.Database.Interfaces;

public interface IOrderRepository : IRepository<Order>, IDisposable
{

}