using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(ServerDbContext context) : base(context)
    {
    }
}