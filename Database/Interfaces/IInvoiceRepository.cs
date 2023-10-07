using Server.API.Models;

namespace Server.API.Database.Interfaces;

public interface IInvoiceRepository : IRepository<Invoice>, IDisposable
{
    
}