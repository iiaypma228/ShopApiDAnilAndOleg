using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class ProductRest
{
    public int Id { get; set; }
    
    [ForeignKey("Shop")]
    public int ShopId { get; set; }
    
    [NotMapped]
    public Shop Shop { get; set; } // на каком магазине остаток
    
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    
    [NotMapped]
    public Product Product { get; set; }
    
    public double Amount { get; set; }  // количество товара на остатках
}