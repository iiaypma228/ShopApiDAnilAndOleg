using Server.API.Database.Interfaces;
using Server.API.Models;

namespace Server.API.Database.Repositories;

public class InvoiceProductRepository : Repository<InvoiceProduct>, IInvoiceProductRepository
{
    public InvoiceProductRepository(ServerDbContext context) : base(context)
    {
    }
}