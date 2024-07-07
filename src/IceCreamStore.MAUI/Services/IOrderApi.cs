using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.Services
{
    [Headers("Authorization: Bearer")]
    public interface IOrderApi
    {
        [Post("/api/order/place-order")]
        Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto);
    }
}
