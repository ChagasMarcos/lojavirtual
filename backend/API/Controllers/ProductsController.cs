using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductsRepository repo) : ControllerBase
{

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
       return Ok(await repo.GetProductsAsync(brand, type, sort));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await repo.GetProductByIdAsync(id) is Product product ? Ok(product) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);
        
            if(await repo.SaveAllAsync())
            {        
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            
            return BadRequest("Não foi possível criar o produto");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if(product.Id != id || !ProductExistes(id)) return BadRequest("O id do produto não corresponde ao id da rota");

       repo.UpdateProduct(product);
         if (await repo.SaveAllAsync()) return NoContent();

         return BadRequest("Não foi possível atualizar o produto");
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        
        if (product == null) return NotFound();

        repo.DeleteProduct(product);
        
        if(await repo.SaveAllAsync())
        {      
        return NoContent();
        }
        return BadRequest("Não foi possível deletar o produto");
    }
    private bool ProductExistes(int id)
    {
        return repo.ProductExists(id);
    }

}
