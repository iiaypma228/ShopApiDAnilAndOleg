using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories
{
    public class ContractProviderRepository : Repository<ContractProvider>, IContractProviderRepository
    {
        public ContractProviderRepository(ServerDbContext context) : base(context)
        {
        }
    }
}
