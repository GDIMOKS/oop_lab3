using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Dtos.Product;
using Services.Dtos.Shop;
using Services.Interfaces;

namespace Services.Realization;

public class ProductService : IProductService
{
    private ShopDbContext _dbContext;

    public ProductService(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public string AddProduct(string name)
    {
        var product = new Product {Name = name}; 
        var newProduct = _dbContext.Products.Add(product);
        _dbContext.SaveChanges();

        return newProduct.Entity.Name;
    }

    public ShopDto? GetCheapShop(string name)
    {
        var shop = _dbContext.ShopProducts
            .Include(x => x.Shop)
            .Where(x => x.ProductName == name)
            .OrderBy(x => x.Price)
            .FirstOrDefault();
        
        if (shop == null) return new ShopDto();
        
        return new ShopDto() {Code = shop.ShopCode, Name = shop.Shop.Name, Address = shop.Shop.Address};
    }

}