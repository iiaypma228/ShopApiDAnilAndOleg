using System.Linq.Expressions;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class InvoiceProductService : Service, IInvoiceProductService
{
    public InvoiceProductService(IUnitOfWork uow) : base(uow)
    {
    }

    public void Save(InvoiceProduct item)
    {
        this.Save(new List<InvoiceProduct>(){item});
    }

    public void Save(IList<InvoiceProduct> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.InvoiceProductRepository.Read(i => i.Id == item.Id).FirstOrDefault();

            if (exs != null)
                this.uow.InvoiceProductRepository.Update(item);
            else
                this.uow.InvoiceProductRepository.Create(item);
        }
    }

    public IList<InvoiceProduct> Read()
    {
        return this.uow.InvoiceProductRepository.Read().ToList();
    }

    public IList<InvoiceProduct> Read(Expression<Func<InvoiceProduct, bool>> where)
    {
        return this.uow.InvoiceProductRepository.Read(where).ToList();
    }

    public InvoiceProduct Read(object id)
    {
        return this.uow.InvoiceProductRepository.Read(i => i.Id == (string)id).FirstOrDefault();
    }


    public void SaveLink(string invoiceId, List<InvoiceProduct> items)
    {
        var olds = this.uow.InvoiceProductRepository.Read(i => i.InvoiceId == invoiceId).ToList();

        foreach (var item in items)
        {
            var old = olds.Where(i => i.Id == item.Id).FirstOrDefault();

            if (old != null)
                olds.Remove(old);
        }
        this.Save(items);
        this.Delete(olds);
    }
    public void Delete(InvoiceProduct item)
    {
        this.Delete(new List<InvoiceProduct>() {item});
    }

    public void Delete(IList<InvoiceProduct> items)
    {
        foreach (var item in items)
        {
            this.uow.InvoiceProductRepository.Delete(item);
        }
        
    }
}