using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ServerDbContext context) : base(context)
    {
    }
}