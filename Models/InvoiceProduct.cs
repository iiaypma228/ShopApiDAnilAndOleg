using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class InvoiceProduct
{
    public string? Id { get; set; }
    
    [ForeignKey("Invoice")]
    public string? InvoiceId { get; set; }
    
    [NotMapped]
    public Invoice? Invoice { get; set; }
    
    [ForeignKey("Product")]
    public int? ProductId { get; set; }
    
    [NotMapped]
    public Product Product { get; set; }
    
    public double? Amount { get; set; }
    

}