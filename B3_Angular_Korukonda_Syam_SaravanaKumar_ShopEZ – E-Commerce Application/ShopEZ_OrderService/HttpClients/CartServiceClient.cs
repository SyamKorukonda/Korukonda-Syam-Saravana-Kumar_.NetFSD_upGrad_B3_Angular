using System.Text.Json;
using ShopEZ_OrderService.Common;
using ShopEZ_OrderService.DTOs;

namespace ShopEZ_OrderService.HttpClients
{

    // HTTP client to communicate with CartService
    // OrderService calls CartService to get cart items when placing order from cart
    public interface ICartServiceClient
    {
        Task<CartSummaryDto> GetCartAsync(string token);
        Task ClearCartAsync(string token);
    }
    public class CartServiceClient:ICartServiceClient
    {
        // HttpClient injected by ASP.NET Core — configured in Program.cs
        private readonly HttpClient _httpClient;

        // JSON options — case insensitive to handle different naming conventions
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Constructor — Dependency Injection
        public CartServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get cart items for logged in user from CartService
        public async Task<CartSummaryDto> GetCartAsync(string token)
        {
            // Create request with Authorization header — forwards user token to CartService
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/cart");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);

            // Throw if cart is empty or not found
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new KeyNotFoundException("Cart not found.");

            // Throw if CartService returns any other error
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"CartService error: {response.StatusCode}");

            // Deserialize response into ApiResponse wrapper
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<ApiResponse<CartSummaryDto>>(json, _jsonOptions);

            if (wrapper?.Data == null || !wrapper.Data.Items.Any())
                throw new InvalidOperationException("Cart is empty. Please add products to cart first.");

            return wrapper.Data;
        }

        // Clear cart after order is placed successfully
        public async Task ClearCartAsync(string token)
        {
            // Create request with Authorization header — forwards user token to CartService
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/cart/clear");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);

            // Log warning if cart clear fails — order is already placed so don't throw
            if (!response.IsSuccessStatusCode)
                Console.WriteLine($"[CartServiceClient] Warning: Failed to clear cart after order. Status: {response.StatusCode}");
        }
    }
}
