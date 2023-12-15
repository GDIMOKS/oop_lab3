using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Dtos.Product;
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
    


}