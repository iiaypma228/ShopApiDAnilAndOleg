using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Services.Classes;

namespace Server.API.Database;

public class ServerDbContext : DbContext
{
    public ServerDbContext(DbContextOptions<ServerDbContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        if (Database.EnsureCreated())
        {
            this.Shop.Add(new Shop()
            {
                Id = -5,
                Name = "Центральний офіс",
                LocationAdress = "Центарльний офіс",
                ShopPhoneNumber = "xxx",
            });

            this.Provider.Add(new Provider()
            {
                Id = -4,
                Name = "Центральний офіс",
                Address = "Центарльний офіс",
                BankName = "xxx",
                BankAccount = "xxx",
                MFO = "xxx"
            });

            this.SaveChanges();

            new EmployeeService(new UnitOfWork(this)).Save(new Employee()
            {
                Name = "admin",
                ShopId = -5,
                Surname = "admin",
                Password = "admin",
                Email = "admin@admin.com",
                Phone = "380971701092",
                Patronymic = "adminovich"
            });
            new EmployeeService(new UnitOfWork(this)).Save(new Employee()
            {
                Name = "oleg",
                ProviderId = -4,
                Surname = "grunt",
                Password = "grunt",
                Email = "grunt200@gmail.com",
                Phone = "380953013828",
                Patronymic = "andreiivich"
            });
        }
    }
    
    public DbSet<Employee> Employee { get; set; }
    
    public DbSet<Invoice> Invoice { get; set; }
    
    public DbSet<InvoiceProduct> InvoiceProduct { get; set; }
    
    public DbSet<Order> Order { get; set; }
    
    public DbSet<OrderProduct> OrderProduct { get; set; }
    
    public DbSet<Product> Product { get; set; }
    
    public DbSet<ProductRest> ProductRest { get; set; }
    
    public DbSet<Shop> Shop { get; set; }

    public DbSet<CategoryProduct> CategoryProduct { get; set; }
    
    public DbSet<SaleProduct> SaleProduct { get; set; }

    public DbSet<Provider> Provider { get; set; }

    public DbSet<ContractProvider> ContractProvider { get; set; }

    public DbSet<ContractProviderProduct> ContractProviderProduct { get; set; }
}