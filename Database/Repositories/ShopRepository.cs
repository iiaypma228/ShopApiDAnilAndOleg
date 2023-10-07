using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class ShopRepository : Repository<Shop>, IShopRepository
{
    public ShopRepository(ServerDbContext context) : base(context)
    {
    }
}