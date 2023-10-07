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
}