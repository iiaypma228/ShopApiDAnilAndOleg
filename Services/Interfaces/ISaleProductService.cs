using Server.API.Models;

namespace Server.API.Services.Interfaces;

public interface ISaleProductService : ICRUDService<SaleProduct>
{
    void SaveSale(IList<SaleProduct> sale);
}