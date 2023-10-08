using Server.API.Models;

namespace Server.API.Services.Interfaces;

public interface IInvoiceService  : ICRUDService<Invoice>
{
    List<InvoiceProduct> ReadProduct(string invoiceId);

    List<Invoice> ReadInvoiceByShopId(int shopId);

    List<Invoice> ReadAllInvoice();

    Shop ReadShop(int id);

    Employee ReadEmployee(int id);
    void MoveInvoice(string id);

}