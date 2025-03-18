namespace API.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public int ErrorCode { get; set; }  // Optional error code
        public string StackTrace { get; set; }  // Optional stack trace

        public static Result<T> Success(T data)
        {
            return new Result<T> { IsSuccess = true, Data = data };
        }

        public static Result<T> Failure(string errorMessage, int errorCode = 0, string stackTrace = null)
        {
            return new Result<T> { IsSuccess = false, ErrorMessage = errorMessage, ErrorCode = errorCode, StackTrace = stackTrace };
        }
    }
}