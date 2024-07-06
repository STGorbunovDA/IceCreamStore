using IceCreamStore.API.Services;
using IceCreamStore.Shared.Dtos;
using System.Security.Claims;

namespace IceCreamStore.API.Endpoints
{
    public static class Endpoints
    {
        private static Guid GetUserId(this ClaimsPrincipal principal) =>
             Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/signup",
                async (SignupRequestDto dto, AuthService authService) =>
                    TypedResults.Ok(await authService.SignupAsync(dto)));

            app.MapPost("/api/signin",
                async (SigninRequestDto dto, AuthService authService) =>
                    TypedResults.Ok(await authService.SigninAsync(dto)));

            app.MapGet("/api/icecreams", 
                async(IcecreamService icecreamService) =>
                TypedResults.Ok(await icecreamService.GetIcecreamsAsync()));

            var userGroup = app.MapGroup("/api/user").RequireAuthorization();

            userGroup.MapPost("/place-order",
                async (OrderPlaceDto dto, ClaimsPrincipal principal, OrderService orderService) =>
                await orderService.PlaceOrderAsync(dto, principal.GetUserId()));

            return app;    
        }
    }
}
