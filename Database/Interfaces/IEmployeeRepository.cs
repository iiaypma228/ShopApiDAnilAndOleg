using Server.API.Models;

namespace Server.API.Database.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>, IDisposable
{
    
}