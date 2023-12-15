using System.ComponentModel.DataAnnotations;

namespace Models;

public class Product
{
    [Key]
    public string Name { get; set; }
    
    public List<Shop> Shops { get; set; } = new();
    public List<ShopProduct> ShopProducts { get; set; } = new();
}