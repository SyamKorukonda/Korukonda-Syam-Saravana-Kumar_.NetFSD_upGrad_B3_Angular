using System.Text.Json;
using ShopEZ_CartService.Common;
using ShopEZ_CartService.DTOs;

namespace ShopEZ_CartService.HttpClients
{
    // HTTP client to communicate with ProductService
    // CartService does NOT share DB with ProductService — calls its REST API instead
    public interface IProductServiceClient
    {
        Task<ProductInfoDto> GetProductAsync(int productId);
    }
    public class ProductServiceClient:IProductServiceClient
    {
        // HttpClient injected by ASP.NET Core — configured in Program.cs
        private readonly HttpClient _httpClient;

        // JSON options — case insensitive to handle different naming conventions
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Constructor — Dependency Injection
        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get product details from ProductService
        public async Task<ProductInfoDto> GetProductAsync(int productId)
        {
            var url = $"/api/products/{productId}";

            // Call ProductService GET api/products/{id}
            var response = await _httpClient.GetAsync(url);

            // Return friendly message if product not found
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new KeyNotFoundException($"Product with ID {productId} was not found. Please check the product ID.");

            // Throw if ProductService returns any other error
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"ProductService error: {response.StatusCode}");

            // Deserialize response into ApiResponse wrapper
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<ApiResponse<ProductInfoDto>>(json, _jsonOptions);

            if (wrapper?.Data == null)
                throw new KeyNotFoundException($"Product with ID {productId} was not found.");

            return wrapper.Data;
        }
    }
}
