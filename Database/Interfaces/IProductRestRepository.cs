using Server.API.Models;

namespace Server.API.Database.Interfaces;

public interface IProductRestRepository : IRepository<ProductRest>, IDisposable
{
    
}