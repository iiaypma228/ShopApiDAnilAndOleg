using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class InvoiceService : Service, IInvoiceService
{
    public InvoiceService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(Invoice item)
    {
        this.Save(new List<Invoice>() {item});
    }

    public void Save(IList<Invoice> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.InvoiceRepository.Read(i => i.Id == item.Id).FirstOrDefault();
            
            if(exs != null)
                this.uow.InvoiceRepository.Update(item);
            else
                this.uow.InvoiceRepository.Create(item);
            
            if(item.InvoiceProducts != null) new InvoiceProductService(this.uow).SaveLink(item.Id, item.InvoiceProducts);
        }
        
        this.uow.Save();
    }

    public IList<Invoice> Read()
    {
        return this.uow.InvoiceRepository.Read().ToList();
    }

    public IList<Invoice> Read(Expression<Func<Invoice, bool>> where)
    {
        return this.uow.InvoiceRepository.Read(where).ToList();
    }

    public Invoice Read(object id)
    {
        return this.uow.InvoiceRepository.Read(i => i.Id == (string)id).FirstOrDefault();
    }

    public void Delete(Invoice item)
    {
        this.Delete(new List<Invoice>(){item} );
    }

    public void Delete(IList<Invoice> items)
    {
        foreach (var item in items)
        {
            this.uow.InvoiceRepository.Delete(item);
        }
        this.uow.Save();
    }

    public List<InvoiceProduct> ReadProduct(string invoiceId)
    {
        return this.uow.InvoiceProductRepository.Read(i => i.InvoiceId == invoiceId)
            .GroupJoin(this.uow.ProductRepository.Read(), op => op.ProductId, p => p.Id,
                (op, p) => new { op, p })
            .SelectMany(o => o.p.DefaultIfEmpty(), (op, p) => new { op.op, p })
            .Select(o => new InvoiceProduct
            {
                Id = o.op.Id,
                Amount = o.op.Amount,
                InvoiceId = o.op.InvoiceId,
                Invoice = o.op.Invoice,
                Product = o.p,
                ProductId = o.op.ProductId,
                
            }).ToList();
    }

    public List<Invoice> ReadInvoiceByShopId(int shopId)
    {
        var res = this.uow.InvoiceRepository.Read(i => i.ShopInId == shopId || i.ShopOutId == shopId)
            .GroupJoin(this.uow.EmployeeRepository.Read(), or => or.EmployeeId, em => em.Id,
                (or, em) => new {or, em})
            .SelectMany(o => o.em.DefaultIfEmpty(), (or, em) => new {or.or, em})
            //left join
            .GroupJoin(this.uow.ShopRepository.Read(), o =>o.or.ShopOutId, sh => sh.Id,
                (o, sh) => new {o.or, o.em , sh})
            .SelectMany(o => o.sh.DefaultIfEmpty(), (o, sh) => new {o.or, o.em, sh})
            //left join
            .GroupJoin(this.uow.ShopRepository.Read(), o =>o.or.ShopInId, shIn => shIn.Id,
                (o, shIn) => new {o.or, o.em , o.sh, shIn})
            .SelectMany(o => o.shIn.DefaultIfEmpty(), (o, shIn) => new {o.or, o.em, o.sh, shIn})
            .Select(ob => new Invoice
            {
                Id = ob.or.Id,
                Date = ob.or.Date,
                EmployeeId = ob.or.EmployeeId,
                Employee = ob.em,
                ShopIn = ob.shIn,
                ShopInId = ob.or.ShopInId,
                ShopOut = ob.sh,
                ShopOutId = ob.or.ShopOutId,
                Number = ob.or.Number,
                Status = ob.or.Status,
                
            }).ToList();
        return res;
    }

    public List<Invoice> ReadAllInvoice()
    {
        var res = this.uow.InvoiceRepository.Read()
            .GroupJoin(this.uow.EmployeeRepository.Read(), or => or.EmployeeId, em => em.Id,
                (or, em) => new {or, em})
            .SelectMany(o => o.em.DefaultIfEmpty(), (or, em) => new {or.or, em})
            //left join
            .GroupJoin(this.uow.ShopRepository.Read(), o =>o.or.ShopOutId, sh => sh.Id,
                (o, sh) => new {o.or, o.em , sh})
            .SelectMany(o => o.sh.DefaultIfEmpty(), (o, sh) => new {o.or, o.em, sh})
            //left join
            .GroupJoin(this.uow.ShopRepository.Read(), o =>o.or.ShopInId, shIn => shIn.Id,
                (o, shIn) => new {o.or, o.em , o.sh, shIn})
            .SelectMany(o => o.shIn.DefaultIfEmpty(), (o, shIn) => new {o.or, o.em, o.sh, shIn})
            .Select(ob => new Invoice
            {
                Id = ob.or.Id,
                Date = ob.or.Date,
                EmployeeId = ob.or.EmployeeId,
                Employee = ob.em,
                ShopIn = ob.shIn,
                ShopInId = ob.or.ShopInId,
                ShopOut = ob.sh,
                ShopOutId = ob.or.ShopOutId,
                Number = ob.or.Number,
                Status = ob.or.Status,
                
            }).ToList();
        return res;
    }

    public Shop ReadShop(int id)
    {
        return this.uow.ShopRepository.Read(i => i.Id == id).FirstOrDefault();
    }

    public Employee ReadEmployee(int id)
    {
        return this.uow.EmployeeRepository.Read(i => i.Id == id).FirstOrDefault();
    }
}