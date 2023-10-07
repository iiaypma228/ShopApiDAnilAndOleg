using Server.API.Database;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class ServiceContainer : IServiceContainer
{
    public ServerDbContext _context { get; }
    
    public ServiceContainer(ServerDbContext context)
    {
        this._context = context;
    }
    
    public T Resolve<T>(string name = null)
    {
        return (T)this.container[typeof(T)](this);
    }

    private readonly IDictionary<Type, Func<IServiceContainer, Object>> container =
        new Dictionary<Type, Func<IServiceContainer, Object>>()
        {
            { typeof(IEmployeeService), (o) => { return new EmployeeService(new UnitOfWork(o._context)); } },
            { typeof(IInvoiceService), (o) => { return new InvoiceService(new UnitOfWork(o._context)); } },
            { typeof(IInvoiceProductService), (o) => { return new InvoiceProductService(new UnitOfWork(o._context)); } },
            { typeof(IOrderService), (o) => { return new OrderService(new UnitOfWork(o._context)); } },
            { typeof(IOrderProductService), (o) => { return new OrderProductService(new UnitOfWork(o._context)); } },
            { typeof(IProductService), (o) => { return new ProductService(new UnitOfWork(o._context)); } },
            { typeof(IProductRestService), (o) => { return new ProductRestService(new UnitOfWork(o._context)); } },
            { typeof(IShopService), (o) => { return new ShopService(new UnitOfWork(o._context)); } },
        };
}