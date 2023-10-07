using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class Order
{
    public string Id { get; set; }
    
    public string Number { get; set; }
    
    public DateTime Date { get; set; }
    
    [ForeignKey("Shop")]
    public int ShopId { get; set; }
    
    [NotMapped]
    public Shop? Shop { get; set; } //заявка от какого магазина
    
    [ForeignKey("Employee")]
    public int? EmployeeId { get; set; }
    
    [NotMapped]
    public Employee? Employee { get; set; } // иницатор
    
    public  DocumentStatus Status { get; set; }
    
    [NotMapped]
    public List<OrderProduct>? OrderProducts { get; set; }
}