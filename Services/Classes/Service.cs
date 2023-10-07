using Server.API.Database;
using Server.API.Models;

namespace Server.API.Services.Classes;

public abstract class Service
{
    public IUnitOfWork uow { get; private set; }

    public Service(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public Shop ReadShopById(int id)
    {
        return this.uow.ShopRepository.Read(i => i.Id == id).FirstOrDefault();
    }
    
    
    public virtual void Dispose()
    {
        this.uow.Dispose();
    }
}