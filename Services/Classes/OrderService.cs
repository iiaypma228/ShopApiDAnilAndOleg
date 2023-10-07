using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class OrderService : Service, IOrderService
{
    public OrderService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(Order item)
    {
        this.Save(new List<Order>() {item});
    }

    public void Save(IList<Order> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.OrderRepository.Read(o => o.Id == item.Id).FirstOrDefault();

            if (exs != null) 
                this.uow.OrderRepository.Update(item);
            else
                this.uow.OrderRepository.Create(item);
            
            
            if(item.OrderProducts != null) new OrderProductService(this.uow).SaveLink(item.Id, item.OrderProducts);
        }
        
        this.uow.Save();
    }

    public IList<Order> Read()
    {
        return this.uow.OrderRepository.Read().ToList();
    }

    public IList<Order> Read(Expression<Func<Order, bool>> where)
    {
        return this.uow.OrderRepository.Read(where).ToList();
    }

    public Order Read(object id)
    {
        return this.uow.OrderRepository.Read(i => i.Id == (string)id).FirstOrDefault();
    }

    public void Delete(Order item)
    {
        this.Delete(new List<Order>() {item});
    }

    public void Delete(IList<Order> items)
    {
        foreach (var item in items)
        {
            this.uow.OrderRepository.Delete(item);
            if(item.OrderProducts != null)
                new OrderProductService(this.uow).Delete(item.OrderProducts);
        }
        this.uow.Save();
    }

    public IList<Order> GetOrderByShopId(int shopId)
    {
        var res = this.uow.OrderRepository.Read(i => i.ShopId == shopId)

            .GroupJoin(this.uow.EmployeeRepository.Read(), or => or.EmployeeId, em => em.Id,
                (or, em) => new {or, em})
            .SelectMany(o => o.em.DefaultIfEmpty(), (or, em) => new {or.or, em})
            //left join
            .GroupJoin(this.uow.ShopRepository.Read(), o =>o.or.ShopId, sh => sh.Id,
                (o, sh) => new {o.or, o.em , sh})
            .SelectMany(o => o.sh.DefaultIfEmpty(), (o, sh) => new {o.or, o.em, sh})
            .Select(ob => new Order
            {
                Id = ob.or.Id,
                Date = ob.or.Date,
                EmployeeId = ob.or.EmployeeId,
                Employee = ob.em,
                ShopId = ob.or.ShopId,
                Shop = ob.sh,
                Number = ob.or.Number,
                Status = ob.or.Status,
            }).ToList();
        return res;
    }
    
    public IList<Order> GetAllOrder()
    {      
        var res = this.uow.OrderRepository.Read()

            .GroupJoin(this.uow.EmployeeRepository.Read(), or => or.EmployeeId, em => em.Id,
                (or, em) => new {or, em})
            .SelectMany(o => o.em.DefaultIfEmpty(), (or, em) => new {or.or, em})
            //left join
            .GroupJoin(this.uow.ShopRepository.Read(), o =>o.or.ShopId, sh => sh.Id,
                (o, sh) => new {o.or, o.em , sh})
            .SelectMany(o => o.sh.DefaultIfEmpty(), (o, sh) => new {o.or, o.em, sh})
            .Select(ob => new Order
            {
                Id = ob.or.Id,
                Date = ob.or.Date,
                EmployeeId = ob.or.EmployeeId,
                Employee = ob.em,
                ShopId = ob.or.ShopId,
                Shop = ob.sh,
                Number = ob.or.Number,
                Status = ob.or.Status,
                
            }).ToList();
        return res;
    }

    public void MoveOrder(string id)
    {
        var co = this.uow.ShopRepository.Read(i => i.Id == -5).FirstOrDefault();
        
        var order = this.Read(id);

        order.OrderProducts = this.ReadProduct(id);

        var inovice = new Invoice()
        {
            Id = Guid.NewGuid().ToString(),
            Number = $"ЗА-{order.Number}",
            ShopOutId = -5,
            Date = order.Date,
            EmployeeId = this.uow.EmployeeRepository.Read(i => i.ShopId == co.Id).FirstOrDefault()?.Id,
            OrderId = order.Id,
            ShopInId  = order.ShopId,
            Status = DocumentStatus.Draft,
            InvoiceProducts = new List<InvoiceProduct>()
        };
        
        foreach (var pr in order.OrderProducts )
        {
            var invocieProduct = new InvoiceProduct()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = pr.Amount,
                InvoiceId = inovice.Id,
                ProductId = pr.ProductId
            };
            inovice.InvoiceProducts.Add(invocieProduct);
        }
        new InvoiceService(this.uow).Save(inovice);
        order.Status = DocumentStatus.Move;
        this.Save(order);
    }

    public void UnMoveOrder(string id)
    {
        var invoice = this.uow.InvoiceRepository.Read(i => i.OrderId == id).FirstOrDefault();

        if (invoice.Status == DocumentStatus.Move)
            throw new Exception(
                $"Відміна проведення не можлива, за цим документом створена накладна! Відмініть проведення накладної №{invoice.Number} за {invoice.Date}");

        var order = this.Read(id);
        
        new InvoiceService(this.uow).Delete(invoice);

        order.Status = DocumentStatus.Draft;
        
        this.Save(order);
        
    }

    public List<OrderProduct> ReadProduct(string orderId)
    {
        return this.uow.OrderProductRepository.Read(i => i.OrderId == orderId)
            .GroupJoin(this.uow.ProductRepository.Read(), op => op.ProductId, p => p.Id,
                (op, p) => new { op, p })
            .SelectMany(o => o.p.DefaultIfEmpty(), (op, p) => new { op.op, p })
            .Select(o => new OrderProduct
            {
                Id = o.op.Id,
                Amount = o.op.Amount,
                Order = o.op.Order,
                OrderId = o.op.OrderId,
                Product = o.p,
                ProductId = o.op.ProductId,
                
            }).ToList();
    }
}