using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class ProductService : Service, IProductService
{
    public ProductService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(Product item)
    {
        this.Save(new List<Product>() {item});
    }

    public void Save(IList<Product> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.ProductRepository.Read(i => i.Id == item.Id).FirstOrDefault();
            
            if(exs != null)
                this.uow.ProductRepository.Update(item);
            else
                this.uow.ProductRepository.Create(item);
        }
        this.uow.Save();
        
    }

    public IList<Product> Read()
    {
        return this.uow.ProductRepository.Read().ToList();
    }

    public IList<Product> Read(Expression<Func<Product, bool>> where)
    {
        return this.uow.ProductRepository.Read(where).ToList();
    }

    public Product Read(object id)
    {
        return this.uow.ProductRepository.Read(i => i.Id == (int)id).FirstOrDefault();
    }

    public void Delete(Product item)
    {
        this.Delete(new List<Product>() {item});
    }

    public void Delete(IList<Product> items)
    {
        foreach (var item in items)
        {
            this.uow.ProductRepository.Delete(item);
        }
        this.uow.Save();
    }
}