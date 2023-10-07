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
    
}