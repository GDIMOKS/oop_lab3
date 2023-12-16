using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Dtos.Product;
using Services.Dtos.Shop;
using Services.Interfaces;

namespace oop_lab3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public void AddProduct([FromBody]ProductDto dto)
    {
        _productService.AddProduct(dto.Name);
    }

    [HttpGet("/cheap/{name}")]
    public IActionResult GetCheapShop([FromRoute]string name)
    {
        var shop = _productService.GetCheapShop(name);
        return (shop != null) ? Ok(shop) : BadRequest("Данного товара в магазинах не существует!");
    }

}