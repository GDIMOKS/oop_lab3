using System.Net;
using DataAccess;
using Models;
using Services.Dtos.Product;
using Services.Interfaces;

namespace Services.Realization;

public class ShopService : IShopService
{
    private ShopDbContext _dbContext;

    public ShopService(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public int AddShop(int code, string name, string address)
    {
        var shop = new Shop {Code = code, Name = name, Address = address}; 
        var newShop = _dbContext.Shops.Add(shop);
        _dbContext.SaveChanges();

        return newShop.Entity.Code;
    }

    public void AddProductsSupply(int shopCode, List<ProvideProductsDto> dtos)
    {
        var shop = _dbContext.Shops.FirstOrDefault(x => x.Code == shopCode);
        if (shop == null)
            return;
        
        var shopProducts = dtos.Select(x => new ShopProduct
        {
            ShopCode = shopCode,
            ProductName = x.ProductName,
            Price = x.Price,
            Quantity = x.Quantity
        }).ToList();

        _dbContext.ShopProducts.AddRange(shopProducts);
        _dbContext.SaveChanges();
    }
}