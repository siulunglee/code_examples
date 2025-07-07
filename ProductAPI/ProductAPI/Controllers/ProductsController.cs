using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;
        private ProductService _ps;

        public ProductsController(ProductContext context)
        {
            _context = context;
            _ps = new ProductService(_context); 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _ps.GetProductList();

            return (Ok(products));
            
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _ps.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySearchTerm(string searchTerm)
        {
            // Fix the CS0029 error by changing the return type of GetProductsBySearchTerm in IProductService to Task<List<Product>>
            var productList = await _ps.GetProductsBySearchTerm(searchTerm);

            // Use LINQ to filter products based on the search term
            //var products = productList.Where(r => r.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            return productList; // Wrap the result in Ok() to return ActionResult<IEnumerable<Product>>
        }


        // Fix for CS1002: ; expected and CS1513: } expected
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            try
            {
                var updateProductEntity = _ps.UpdateProduct(product);

                if (updateProductEntity.Id == id)
                {
                    return Ok(new { product }); // Fixed syntax for object initialization
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product."); // Added proper return statement
            }

            return BadRequest("Product ID mismatch."); // Added a return statement to handle cases where IDs do not match
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Task<ActionResult<Product>> PostProduct(Product product)
        {
            _ps.AddProduct(product);

            return Task.FromResult<ActionResult<Product>>(CreatedAtAction("GetProduct", new { id = product.Id }, product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteProduct(int id)
        {
            bool productDeleted = _ps.DeleteProduct(id);
            if (productDeleted == false)
            {
                return Task.FromResult<IActionResult>(NotFound());
            }

            return Task.FromResult<IActionResult>(NoContent());
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }


        [HttpGet("AddSampleData")]
        public async Task<IActionResult> AddSampleData()
        {
            if (_context.Products.Any())
            {
                return BadRequest("Sample data already exists.");
            }
            var sampleProducts = new List<Product>
            {
                new Product { Name = "Sample Product 1", Description = "Description for Sample Product 1", Price = 10.99, StockQty = 100, Category = "Category1", ImageUrl = "1.png" },
                new Product { Name = "Sample Product 2", Description = "Description for Sample Product 2", Price = 20.99, StockQty = 50, Category = "Category2", ImageUrl = "2.png" },
                new Product { Name = "Sample Product 1a", Description = "Description for Sample Product 1a", Price = 30.99, StockQty = 75, Category = "Category1a", ImageUrl = "1.png" },
                new Product { Name = "Sample Product 3", Description = "Description for Sample Product 3", Price = 40.99, StockQty = 175, Category = "Category3", ImageUrl = "3.png" },
                new Product { Name = "Sample Product 4", Description = "Description for Sample Product 4", Price = 30.99, StockQty = 75, Category = "Category4", ImageUrl = "2.png" },
            };
            _context.Products.AddRange(sampleProducts);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
