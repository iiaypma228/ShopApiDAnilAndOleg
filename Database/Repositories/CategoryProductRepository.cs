using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories
{
    public class CategoryProductRepository : Repository<CategoryProduct>, ICategoryProductRepository
    {
        public CategoryProductRepository(ServerDbContext context) : base(context)
        {
        }
    }
}
