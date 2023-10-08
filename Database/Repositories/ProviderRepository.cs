using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(ServerDbContext context) : base(context)
        {
        }
    }
}
