using Server.API.Database;

namespace Server.API.Services.Interfaces;

public interface IServiceContainer
{
    ServerDbContext _context { get; }
    T Resolve<T>(string name = null);
}