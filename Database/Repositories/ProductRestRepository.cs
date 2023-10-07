using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class ProductRestRepository : Repository<ProductRest>, IProductRestRepository
{
    public ProductRestRepository(ServerDbContext context) : base(context)
    {
    }
}