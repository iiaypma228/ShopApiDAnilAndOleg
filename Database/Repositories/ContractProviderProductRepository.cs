using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories
{
    public class ContractProviderProductRepository : Repository<ContractProviderProduct>, IContractProviderProductRepository
    {
        public ContractProviderProductRepository(ServerDbContext context) : base(context)
        {
        }
    }
}
