namespace WebApplication17.ApiResponse
{
    // Generic response wrapper class
    // Used to standardize all API responses (Success / Error) with success status, message, and data.
    public class ApiResponse<T>
    {
        // Indicates whether the request was successful or not

        public bool Success { get; set; }

        // Message describing the result (success or error)

        public string Message { get; set; }

        // Generic data returned from API (can be any type)
        public T Data { get; set; }

        //Constructor for Sucess response
        // Used when API returns data
        public ApiResponse(T data, string message = "Success")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor for ERROR response
        // Used when API fails (no data)
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }

    }
}
