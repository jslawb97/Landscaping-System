namespace RestApi.Models
{
    /// <summary>
    /// Wrapper class for API Responses
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/4/2018
    /// </remarks>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Whether or not the API request was successful
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public bool Success { get; set; }

        /// <summary>
        /// A Message to include in the ApiResponse
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public string Message { get; set; }

        /// <summary>
        /// A serializable object to include in the ApiResponse
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public T Data { get; set; }

        /// <summary>
        /// ApiResponse Constructor for Errors
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <param name="errorMessage">A Message to include in the ApiResponse</param>
        public ApiResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
        }

        /// <summary>
        /// ApiResponse Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="success">Whether or not the API request was successful</param>
        /// <param name="message">A Message to include in the ApiResponse</param>
        public ApiResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// ApiResponse Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <param name="success">Whether or not the API request was successful</param>
        /// <param name="message">A Message to include in the ApiResponse</param>
        /// <param name="data">A serializable object to include in the ApiResponse</param>
        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public override string ToString()
        {
            return Data != null ? Data.ToString() : Message;
        }
    }
}