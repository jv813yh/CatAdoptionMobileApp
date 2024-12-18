namespace CatAdoptionMobileApp.Shared.Dtos
{
    public record ApiResponse(bool IsSuccess, string? Message = null)
    {
        public static ApiResponse Success() 
            => new ApiResponse(true);
        public static ApiResponse Fail(string message) 
            => new ApiResponse(false, message);
    }

    public record ApiResponse<T>(bool IsSuccess, T? Data, string? Message = null)
    {
        public static ApiResponse<T> Success(T data) 
            => new ApiResponse<T>(true, data);
        public static ApiResponse<T> Fail(string message) 
            => new ApiResponse<T>(false, default, message);
    }
}
