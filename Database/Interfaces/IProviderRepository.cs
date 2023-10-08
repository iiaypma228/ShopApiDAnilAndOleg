using Server.API.Models;

namespace Server.API.Database.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>, IDisposable
    {
    }
}
