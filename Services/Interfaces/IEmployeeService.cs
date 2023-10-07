using Server.API.Models;

namespace Server.API.Services.Interfaces;

public interface IEmployeeService : ICRUDService<Employee>
{
    Employee AuthUser(string email, string password);
}