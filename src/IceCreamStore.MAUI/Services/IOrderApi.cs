using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.Services
{
    [Headers("Authorization: Bearer")]
    public interface IOrderApi
    {
        [Post("/api/orders/place-order")]
        Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto);

        [Get("/api/orders")]
        Task<OrderDto[]> GetMyOrdersAsync();

        [Get("/api/orders/{orderId}/items")] //TODO почему {orderId}
        Task<OrderItemDto[]> GetOrderItemsAsync(long orderId);
    }
}
