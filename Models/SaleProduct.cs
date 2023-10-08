using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class SaleProduct
{
    public string Id { get; set; }
    
    [ForeignKey("Shop")]
    public int ShopId { get; set; }
    
    [NotMapped]
    public Shop? Shop { get; set; }
    
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    
    [NotMapped]
    public Product? Product { get; set; }
    
    public decimal Price { get; set; }

    public double Amount { get; set; }
    
    public DateTime Date { get; set; }

}