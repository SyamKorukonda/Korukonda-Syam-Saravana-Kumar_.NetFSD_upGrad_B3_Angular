using System.Text.Json;
using Consumer_Service.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Consumer_Service.Services
{
    public class ProductApiService:IProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7233/api/Products");
            var jsonData=await response.Content.ReadAsStringAsync();
           
            // It converts JSON strings into C# objects
            // Use JsonSerializer.Deserialize<T>(jsonString) for direct conversion


            var options =new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var productData=JsonSerializer.Deserialize<List<Product>>(jsonData,options);
            return productData;


        }
        public async Task<Product> GetProductId(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7233/api/Products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch product");
            }


            // It converts JSON strings into C# objects
            // Use JsonSerializer.Deserialize<T>(jsonString) for direct conversion


            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            //var productData = JsonSerializer.Deserialize<Product>(jsonData, options);
            var productData = await response.Content.ReadFromJsonAsync <Product >(options);
            return productData;
        }
        
        public async Task<Product> AddProduct(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7233/api/Products", product);

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var jsonData=await response.Content.ReadFromJsonAsync<Product>(options);
            return jsonData;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7233/api/Products/{product.Id}", product);

            if (!response.IsSuccessStatusCode)
            {
                // Read the error message from the API to see why it failed (400, 404, or 405)
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Update failed: {response.StatusCode} - {errorContent}");
            }

            // 2. Handle '204 No Content'
            // Many APIs return 204 on success. If so, there is no JSON to read.
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return product;
            }

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            // 3. Read the returned object if the API sends it back (Status 200 OK)
            var jsonData = await response.Content.ReadFromJsonAsync<Product>(options);
            return jsonData;
        }
        public async Task<Product> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7233/api/Products/{id}");
           
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            var jsonData=await response.Content.ReadFromJsonAsync<Product>( options );
            return jsonData;
        }
    }
}
