using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
{

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var spec = new ProductFilterSpecification(specParams);
        return await CreatePageResult(repo, spec, specParams.PageIndex, specParams.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await repo.GetByIdAsync(id) is Product product ? Ok(product) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        
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

       repo.Update(product);
         if (await repo.SaveAllAsync()) return NoContent();

         return BadRequest("Não foi possível atualizar o produto");
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        
        if (product == null) return NotFound();

        repo.Remove(product);
        
        if(await repo.SaveAllAsync())
        {      
        return NoContent();
        }
        return BadRequest("Não foi possível deletar o produto");
    }
    private bool ProductExistes(int id)
    {
        return repo.Exists(id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<string>>> GetBrands()
    {
        // var brands = await repo.ListAllAsync();
        // return Ok(brands.Select(p => p.Brand).Distinct());

        var spec = new BrandListSpacification();
        return Ok(await repo.ListAsync(spec));
        
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<string>>> GetTypes()
    {
        // var types = await repo.ListAllAsync();
        // return Ok(types.Select(p => p.Type).Distinct());

        var spec = new TypeListSpecification();
        return Ok(await repo.ListAsync(spec));
    }
}
