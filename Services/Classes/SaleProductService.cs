using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Database.Interfaces;
using Server.API.Database.Repositories;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class SaleProductService : Service, ISaleProductService
{
    public SaleProductService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(SaleProduct item)
    {
        this.Save(new List<SaleProduct>() {item});
    }

    public void Save(IList<SaleProduct> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.SaleProductRepository.Read(i => i.Id == item.Id).FirstOrDefault();
            
            if(exs != null)
                this.uow.SaleProductRepository.Update(item);
            else
                this.uow.SaleProductRepository.Create(item);
        }
        this.uow.Save();
    }

    public IList<SaleProduct> Read()
    {
        return this.uow.SaleProductRepository.Read().ToList();
    }

    public IList<SaleProduct> Read(Expression<Func<SaleProduct, bool>> where)
    {
        return this.uow.SaleProductRepository.Read(where).ToList();
    }

    public SaleProduct Read(object id)
    {
        return this.uow.SaleProductRepository.Read(i => i.Id == (string)id).FirstOrDefault();
    }

    public void Delete(SaleProduct item)
    {
        this.Delete(new List<SaleProduct>(){item});
    }

    public void Delete(IList<SaleProduct> items)
    {
        foreach (var item in items)
        {
            this.uow.SaleProductRepository.Delete(item);
        }
    }

    public void SaveSale(IList<SaleProduct> sale)
    {
        foreach (var s in sale)
        {
            s.Product = this.uow.ProductRepository.Read(i => i.Id == s.ProductId).FirstOrDefault();
            
            var rest = this.uow.ProductRestRepository.Read(i => i.ProductId == s.ProductId
                                                                && i.Date <= s.Date && i.ShopId == s.ShopId).FirstOrDefault();

            if (rest == null)
                throw new Exception(
                    $"Для товару {s.Product.ShortName} не вистачає залишків!");
            else if (rest.Amount - s.Amount < 0)
                throw new Exception($"Для товару {s.Product.ShortName} не вистачає залишків! Ще потрібно: {Math.Abs(rest.Amount - s.Amount)}");

            var newRest = new ProductRest()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = rest.Amount - s.Amount,
                Date = s.Date,
                ProductId = s.ProductId,
                ShopId = s.ShopId
            };
            
            this.Save(s);
            
            new ProductRestService(this.uow).Save(newRest);
            
        }
        this.uow.Save();
    }
}