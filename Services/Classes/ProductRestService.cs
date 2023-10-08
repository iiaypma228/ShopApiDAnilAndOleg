using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class ProductRestService : Service, IProductRestService
{
    public ProductRestService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(ProductRest item)
    {
        this.Save(new List<ProductRest>() {item});
    }

    public void Save(IList<ProductRest> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.ProductRestRepository.Read(i => i.Id == item.Id).FirstOrDefault();
            
            if(exs != null)
                this.uow.ProductRestRepository.Update(item);
            else
                this.uow.ProductRestRepository.Create(item);
        }
        this.uow.Save();
    }

    public IList<ProductRest> Read()
    {
        return this.uow.ProductRestRepository.Read().ToList();
    }

    public IList<ProductRest> Read(Expression<Func<ProductRest, bool>> where)
    {
        return this.uow.ProductRestRepository.Read(where).ToList();
    }

    public ProductRest Read(object id)
    {
        return this.uow.ProductRestRepository.Read(i => i.Id == (string)id).FirstOrDefault();
    }

    public void Delete(ProductRest item)
    {
        this.Delete(new List<ProductRest>() {item});
    }

    public void Delete(IList<ProductRest> items)
    {
        foreach (var item in items)
        {
            this.uow.ProductRestRepository.Delete(item);
        }
        this.uow.Save();
    }
}