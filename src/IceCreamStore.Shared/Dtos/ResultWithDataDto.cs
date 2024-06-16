namespace IceCreamStore.Shared.Dtos
{
    public record ResultWithDataDto<TData>(bool IsSuccess, TData Data, string? ErrorMessage)
    {
        public static ResultWithDataDto<TData> Success(TData data) => new(true, data, null);
        public static ResultWithDataDto<TData> Failure(string? errorMessage) => new(false, default, errorMessage);
    }
}
