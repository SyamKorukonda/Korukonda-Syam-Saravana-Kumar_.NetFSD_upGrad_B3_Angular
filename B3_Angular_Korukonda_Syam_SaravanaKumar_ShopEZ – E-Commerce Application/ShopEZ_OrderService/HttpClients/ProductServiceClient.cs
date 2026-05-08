using System.Text.Json;
using ShopEZ_OrderService.Common;
using ShopEZ_OrderService.DTOs;

namespace ShopEZ_OrderService.HttpClients
{
    // HTTP client to communicate with ProductService
    public interface IProductServiceClient
    {
        Task<ProductInfoDto> GetProductAsync(int productId);
        Task ReduceStockAsync(int productId, int quantity);
    }

    public class ProductServiceClient : IProductServiceClient
    {
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

            var response = await _httpClient.GetAsync(url);

            // Return friendly message if product not found
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new KeyNotFoundException($"Product with ID {productId} was not found. Please check the product ID.");

            // Handle any other error from ProductService
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"ProductService error: {response.StatusCode}");

            // Deserialize response into ApiResponse wrapper
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<ApiResponse<ProductInfoDto>>(json, _jsonOptions);

            if (wrapper?.Data == null)
                throw new KeyNotFoundException($"Product with ID {productId} was not found. Please check the product ID.");

            return wrapper.Data;
        }

        // Reduce stock in ProductService after order is placed
        public async Task ReduceStockAsync(int productId, int quantity)
        {
            var url = $"/api/products/{productId}/reduce-stock?quantity={quantity}";

            var response = await _httpClient.PatchAsync(url, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ProductServiceClient] PATCH Error: {errorContent}");
                throw new HttpRequestException($"Failed to reduce stock for product {productId}");
            }
        }
    }
}