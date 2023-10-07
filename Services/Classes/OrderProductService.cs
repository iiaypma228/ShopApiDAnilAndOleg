using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class OrderProductService : Service, IOrderProductService
{
    public OrderProductService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(OrderProduct item)
    {
        this.Save(new List<OrderProduct>() {item});
    }

    public void Save(IList<OrderProduct> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.OrderProductRepository.Read(i => i.Id == item.Id).FirstOrDefault();
            
            if(exs != null)
                this.uow.OrderProductRepository.Update(item);
            else
                this.uow.OrderProductRepository.Create(item);
        }
        
    }

    public IList<OrderProduct> Read()
    {
        return this.uow.OrderProductRepository.Read().ToList();
    }

    public IList<OrderProduct> Read(Expression<Func<OrderProduct, bool>> where)
    {
        return this.uow.OrderProductRepository.Read(where).ToList();
    }

    public OrderProduct Read(object id)
    {
        return this.uow.OrderProductRepository.Read(i => i.Id == (string)id).FirstOrDefault();
    }

    public void Delete(OrderProduct item)
    {
        this.Delete(new List<OrderProduct>() {item});
    }

    public void Delete(IList<OrderProduct> items)
    {
        foreach (var item in items)
        {
            this.uow.OrderProductRepository.Delete(item);
        }
        
    }

    public void SaveLink(string orderId, List<OrderProduct> items)
    {
        var olds = this.uow.OrderProductRepository.Read(i => i.OrderId == orderId).ToList();

        foreach (var item in items)
        {
            var old = olds.Where(i => i.Id == item.Id).FirstOrDefault();

            if (old != null)
                olds.Remove(old);
        }
        this.Save(items);
        this.Delete(olds);
    }
}