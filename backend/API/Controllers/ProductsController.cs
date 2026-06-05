using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _repo;

    public ProductsController(IGenericRepository<Product> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var spec = new ProductFilterSpecification(specParams);
        return await CreatePageResult(_repo, spec, specParams.PageIndex, specParams.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await _repo.GetByIdAsync(id) is Product product ? Ok(product) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _repo.Add(product);

        if(await _repo.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        return BadRequest("Não foi possível criar o produto");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if(product.Id != id || !ProductExistes(id)) return BadRequest("O id do produto não corresponde ao id da rota");

        _repo.Update(product);
        if (await _repo.SaveAllAsync()) return NoContent();

        return BadRequest("Não foi possível atualizar o produto");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        _repo.Remove(product);

        if(await _repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Não foi possível deletar o produto");
    }

    private bool ProductExistes(int id)
    {
        return _repo.Exists(id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<string>>> GetBrands()
    {
        var spec = new BrandListSpacification();
        return Ok(await _repo.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await _repo.ListAsync(spec));
    }
}
