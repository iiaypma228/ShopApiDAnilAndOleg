using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class ShopService : Service, IShopService
{
    public ShopService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(Shop item)
    {
        this.Save(new List<Shop>() {item});
    }

    public void Save(IList<Shop> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.ShopRepository.Read(i => i.Id == item.Id).FirstOrDefault();
            
            if(exs != null)
                this.uow.ShopRepository.Update(item);
            else
                this.uow.ShopRepository.Create(item);
        }
        this.uow.Save();
    }

    public IList<Shop> Read()
    {
       return this.uow.ShopRepository.Read().ToList();
    }

    public IList<Shop> Read(Expression<Func<Shop, bool>> where)
    {
        return this.uow.ShopRepository.Read(where).ToList();
    }

    public Shop Read(object id)
    {
        return this.uow.ShopRepository.Read(i => i.Id == (int)id).FirstOrDefault();
    }

    public void Delete(Shop item)
    {
        this.Delete(new List<Shop>() {item});
    }

    public void Delete(IList<Shop> items)
    {
        foreach (var item in items)
        {
            this.uow.ShopRepository.Delete(item);
        }
        this.uow.Save();
        
    }
}