using Newtonsoft.Json;

namespace API.Models
{
    public class PagedResult<T>
    {
        [JsonProperty("items")]  // Cambia el nombre del campo en la respuesta
        public IEnumerable<T> Items { get; set; } = default!;

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("page_number")]
        public int PageNumber { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }

        private Result(bool isSuccess, T data, string errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T data) =>
            new Result<T>(true, data, null);

        public static Result<T> Failure(string errorMessage) =>
            new Result<T>(false, default!, errorMessage);
    }
}
