using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class Invoice
{
    public string? Id { get; set; }
    
    public string? Number { get; set; }
    
    [ForeignKey("Order")]
    public string? OrderId { get; set; }
    
    [NotMapped]
    public Order? Order { get; set; } //накладная будет создаваться на основе заявки
    
    [ForeignKey("ShopOut")]
    public int? ShopOutId { get; set; }
    
    [NotMapped]
    public Shop? ShopOut { get; set; } //магазин откуда
    
    [ForeignKey("ShopIn")]
    public int? ShopInId { get; set; }
    
    [NotMapped]
    public Shop? ShopIn { get; set; } //магазин куда
    
    public DateTime? Date { get; set; }
    
    [ForeignKey("Employee")]
    public int? EmployeeId { get; set; }
    
    [NotMapped]
    public Employee Employee { get; set; }
    
    public  DocumentStatus? Status { get; set; }
    
    [NotMapped]
    public List<InvoiceProduct>? InvoiceProducts { get; set; }
}

public enum DocumentStatus
{
    Draft, //черновик
    InProccess, // в процессе(например товар в пути)
    Move, // накладная выполнена, товар списан с остатков
}