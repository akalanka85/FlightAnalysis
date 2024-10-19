
namespace FlightAnalysis.Models.DTO
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Data = data, IsSuccess = true };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T> { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}
