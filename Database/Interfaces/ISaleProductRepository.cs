using Server.API.Models;

namespace Server.API.Database.Interfaces;

public interface ISaleProductRepository  : IRepository<SaleProduct>, IDisposable
{
    
}