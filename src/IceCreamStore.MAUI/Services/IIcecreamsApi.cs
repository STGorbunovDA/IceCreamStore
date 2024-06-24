using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.Services
{
    public interface IIcecreamsApi
    {
        [Get("/api/icecreams")]
        Task<IcecreamDto[]> GetIcecreamsAsync();
    }
}
