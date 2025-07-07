using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProductAPI.Models;
using ProductAPI.Services;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public class ProductService
    {
        private readonly ProductContext _context;
        
        public ProductService(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductList()
        {
            return await _context.Products.ToListAsync();
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {id} not found.");
            }
            return product;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySearchTerm(string searchTerm)
        {
            List<Product> productList = await _context.Products.ToListAsync();
            List<Product> productMatchedList = productList.Where(r => r.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            return productMatchedList;
        }

        public Product AddProduct(Product product)
        {
            var result = _context.Products.Add(product);
            _context.SaveChanges();
            return result.Entity;
        }

        public Product UpdateProduct(Product product)
        {
            var result = _context.Products.Update(product);
            _context.SaveChanges();

            return result.Entity;
        }

        public bool DeleteProduct(int Id)
        {
            var filteredData = _context.Products.Where(x => x.Id == Id).FirstOrDefault();
            if (filteredData == null)
            {
                return false;
            }
            var result = _context.Products.Remove(filteredData);
            _context.SaveChanges();
            return result != null;
        }
    }
}
