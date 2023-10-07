using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class OrderProduct
{
    public string? Id { get; set; }
    
    [ForeignKey("Order")]
    public string? OrderId { get; set; }
    
    [NotMapped]
    public Order? Order { get; set; }
    
    [ForeignKey("Product")]
    public int? ProductId { get; set; }
    
    [NotMapped]
    public Product? Product { get; set; }
    
    public double? Amount { get; set; }
}