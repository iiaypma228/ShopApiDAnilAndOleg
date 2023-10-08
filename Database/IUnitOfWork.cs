using Server.API.Database.Interfaces;

namespace Server.API.Database;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository EmployeeRepository { get;  }
    
    IInvoiceRepository InvoiceRepository { get; }
    
    IInvoiceProductRepository InvoiceProductRepository { get; }
    
    IOrderRepository OrderRepository { get; }
    
    IOrderProductRepository OrderProductRepository { get; }
    
    IProductRepository ProductRepository { get; }
    
    IProductRestRepository ProductRestRepository { get; }
    
    IShopRepository ShopRepository { get; }

    ICategoryProductRepository CategoryProductRepository { get; }
    ISaleProductRepository SaleProductRepository { get; }

    void Save(); //сохранение, после этого транзакиця завершается и изменения вступают в силу

}