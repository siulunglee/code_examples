using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        
        public Task<IEnumerable<Product>> GetProductList();
        public Product GetProductById(int id);
        public Task<Product> GetProductsBySearchTerm(string searchTerm);
        public Product AddProduct(Product product);
        public Product UpdateProduct(Product product);
        public bool DeleteProduct(int Id);
    }
}
