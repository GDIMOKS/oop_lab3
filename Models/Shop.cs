using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Models;

public class Shop
{
    [Key]
    public int Code { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public List<Product> Products { get; set; } = new();
    public List<ShopProduct> ShopProducts { get; set; } = new();
}