using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class SaleProductRepository : Repository<SaleProduct>, ISaleProductRepository
{
    public SaleProductRepository(ServerDbContext context) : base(context)
    {
    }
}