using Server.API.Models;

namespace Server.API.Database.Interfaces;

public interface IProductRepository : IRepository<Product>, IDisposable
{
    
}