using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Controllers;
using ProductAPI.Models;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace UnitTests
{
    public class UnitTestForWebApi
    {

        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7285") };

        [Fact]
        public async Task searchTermTestAsync()
        {
            // 1. Arrange

            // expected result of the search API call
            List<Product> products = new List<Product>
            {
            new Product
            {
                    Id = 1,
                    Name = "Sample Product 1",
                    Description = "Description for Sample Product 1",
                    Price = 10.99,
                    StockQty = 100,
                    Category = "Category1",
                    ImageUrl = "1.png"
            },
                new Product {
                    Id = 3,
                    Name = "Sample Product 1a",
                    Description = "Description for Sample Product 1a",
                    Price = 30.99,
                    StockQty = 75,
                    Category = "Category1a",
                    ImageUrl = "1.png"                
                },
            };

            // setup sample data
            var apiUrlForAddSampleData = "https://localhost:7285/api/Products/AddSampleData";
            var response_AddSampleData = await _httpClient.GetAsync(apiUrlForAddSampleData);


            // 2. Act

            var apiUrl = "https://localhost:7285/api/Products/search/Product 1";
            var response = await _httpClient.GetAsync(apiUrl);

            // 3. Assert

            response.EnsureSuccessStatusCode();
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            
            // Assertions for the result is not null
            Assert.NotNull(result);
            System.Diagnostics.Debug.WriteLine($"{result!=null} products");

            // Assertions for the 2 counts of products return expected 
            Assert.Equal(2, result.Count);
            System.Diagnostics.Debug.WriteLine($"{result.Count} products");

            // since this search API only search the "Name" field, therefore we only
            // compare the name property of the expected name result and actual name result.
            if (result.Count == 2)
            {
                Assert.Equal(products[0].Name, result[0].Name);
                Assert.Equal(products[1].Name, result[1].Name);
                System.Diagnostics.Debug.WriteLine("Assert.Equal(products[0].Name, result[0].Name): " + products[0].Name.Equals(result[0].Name));
                System.Diagnostics.Debug.WriteLine("Assert.Equal(products[1].Name, result[1].Name): " + products[1].Name.Equals(result[1].Name));
            }


            // Assertions the acutal result for the product names
            // of the expected result
            Assert.Contains(result, p => p.Name == "Sample Product 1");
            Assert.Contains(result, p => p.Name == "Sample Product 1a");

        }
    }



}