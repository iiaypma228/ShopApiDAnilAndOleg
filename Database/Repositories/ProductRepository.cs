using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ServerDbContext context) : base(context)
    {
    }
}