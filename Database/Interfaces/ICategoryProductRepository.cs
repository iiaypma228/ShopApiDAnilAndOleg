using Server.API.Models;

namespace Server.API.Database.Interfaces
{
    public interface ICategoryProductRepository : IRepository<CategoryProduct>, IDisposable
    {
    }
}
