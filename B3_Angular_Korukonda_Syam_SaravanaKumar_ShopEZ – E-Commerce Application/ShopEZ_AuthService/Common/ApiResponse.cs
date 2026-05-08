namespace ShopEZ_AuthService.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        // Success response
        public ApiResponse(T data, string message = "Success")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Error response
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }
    }
}
