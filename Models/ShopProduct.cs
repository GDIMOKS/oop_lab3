using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class ShopProduct
{
    public int ShopCode { get; set; }
    public Shop? Shop { get; set; }
    
    public string ProductName { get; set; }
    public Product? Product { get; set; }
    
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}