using DataAccess;
using Models;
using Services.Dtos.Product;
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



}