using Microsoft.EntityFrameworkCore;
using Server.API.Database.Interfaces;
using Server.API.Database.Repositories;

namespace Server.API.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly ServerDbContext _dbContext;

    public UnitOfWork(ServerDbContext dbContext)
    {
        this._dbContext = dbContext;

        employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(this._dbContext));

        invoiceRepository = new Lazy<IInvoiceRepository>(() => new InvoiceRepository(this._dbContext));

        invoiceProductRepository =
            new Lazy<IInvoiceProductRepository>(() => new InvoiceProductRepository(this._dbContext));

        orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(this._dbContext));

        orderProductRepository = new Lazy<IOrderProductRepository>(() => new OrderProductRepository(this._dbContext));

        productRepository = new Lazy<IProductRepository>(() => new ProductRepository(this._dbContext));

        productRestRepository = new Lazy<IProductRestRepository>(() => new ProductRestRepository(this._dbContext));

        shopRepository = new Lazy<IShopRepository>(() => new ShopRepository(this._dbContext));

        categoryProductRepository = new Lazy<ICategoryProductRepository> (() => new CategoryProductRepository(this._dbContext));

        saleProductRepository = new Lazy<ISaleProductRepository>(() => new SaleProductRepository(this._dbContext));
    }

    private Lazy<IEmployeeRepository> employeeRepository;
    public IEmployeeRepository EmployeeRepository { get {return employeeRepository.Value;} }

    private Lazy<IInvoiceRepository> invoiceRepository;
    public IInvoiceRepository InvoiceRepository { get {return invoiceRepository.Value;} }

    private Lazy<IInvoiceProductRepository> invoiceProductRepository;
    public IInvoiceProductRepository InvoiceProductRepository { get {return invoiceProductRepository.Value;} }
    
    private Lazy<IOrderRepository> orderRepository;
    public IOrderRepository OrderRepository { get {return orderRepository.Value;} }

    private Lazy<IOrderProductRepository> orderProductRepository;
    public IOrderProductRepository OrderProductRepository { get {return orderProductRepository.Value;} }

    private Lazy<IProductRepository> productRepository;
    public IProductRepository ProductRepository { get {return productRepository.Value;} }

    private Lazy<IProductRestRepository> productRestRepository;
    public IProductRestRepository ProductRestRepository { get {return productRestRepository.Value;} }

    private Lazy<IShopRepository> shopRepository;
    public IShopRepository ShopRepository { get {return shopRepository.Value; } }

    private Lazy<ICategoryProductRepository> categoryProductRepository;
    public ICategoryProductRepository CategoryProductRepository { get { return categoryProductRepository.Value; } }

    private Lazy<ISaleProductRepository> saleProductRepository;
    public ISaleProductRepository SaleProductRepository { get {return saleProductRepository.Value; } }

    public void Save()
    {
        this._dbContext.ChangeTracker.AutoDetectChangesEnabled = true;

        this._dbContext.SaveChanges();

        foreach (var entityEntry in this._dbContext.ChangeTracker.Entries())
            if (entityEntry.Entity != null)
                entityEntry.State = EntityState.Detached;
    }

    public void Dispose()
    {
        this._dbContext.Dispose();
    }
}