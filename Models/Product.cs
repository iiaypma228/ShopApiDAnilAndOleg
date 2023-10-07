using System.ComponentModel.DataAnnotations.Schema;

namespace Server.API.Models;

public class Product
{
    public int Id { get; set; }
    
    public string? ShortName { get; set; }
    
    public string? FullName { get; set; }
    
    public int? Article { get; set; }
    
    public string? Bar { get; set; }
    
    public string? MeasureCode { get; set; }
    
    public decimal? Price { get; set; }
    
    public string? Info { get; set; }  // ÏĞÈÌ²ÒÊÈ ÄÎ ÒÎÂÀĞÓ(ÎËÅÃ)

    [ForeignKey("CategoryProduct")]
    public int? CategoryProductId { get; set; } //ÇÎÂÍ²ØÍ²É ÊËŞ× ÄÎ CATEGORYPRODUCT

    [NotMapped]
    public CategoryProduct? CategoryProduct { get; set;}
}