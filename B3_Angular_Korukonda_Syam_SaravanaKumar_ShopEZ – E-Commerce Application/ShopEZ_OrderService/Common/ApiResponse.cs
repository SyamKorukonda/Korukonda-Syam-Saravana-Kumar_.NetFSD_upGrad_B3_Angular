namespace ShopEZ_OrderService.Common
{
    public class ApiResponse<T>
    {
        // Indicates whether the request was successful or not
        public bool Success { get; set; }
        // Message describing the result
        public string Message { get; set; } = string.Empty;
        // Data returned from API — can be any type
        public T? Data { get; set; }

        // Parameterless constructor — required for JSON deserialization in ProductServiceClient
        public ApiResponse() { }

        // Success response — used when API returns data
        public ApiResponse(T data, string message = "Success")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Error response — used when API fails
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }
    }
}