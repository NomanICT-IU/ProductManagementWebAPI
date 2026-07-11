using Microsoft.AspNetCore.Mvc;
using ProductManagementWebAPI.Models;
using ProductManagementWebAPI.Repository;

namespace ProductManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Get all products
        [HttpGet("")]
        public async Task<IActionResult> GetAllProduct()
        {
            var records = await _productRepository.GetAllProductAsync();
            return Ok(records);
        }

        // Get product by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var record = await _productRepository.GetProductByIdAsync(id);
            if (record == null)
                return NotFound();
            return Ok(record);
        }

        //add a new Product
        [HttpPost("")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel model)
        {
            var id = await _productRepository.AddProuctAsync(model);
            return CreatedAtAction(nameof(GetProductById), new { id }, model);
        }

        //update Product
        [HttpPut("{productId:int}")]
        public async Task<IActionResult> UpdateProduct(
         [FromRoute] int productId,
         [FromBody] ProductModel model)
        {
            var updatedProduct = await _productRepository.UpdateProductAsync(productId, model);

            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        //Delete Product
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var deleted = await _productRepository.DeleteProductAsync(productId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
