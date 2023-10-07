using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ServerDbContext context) : base(context)
    {
    }
}