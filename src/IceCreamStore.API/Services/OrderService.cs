using IceCreamStore.API.Data;
using IceCreamStore.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IceCreamStore.API.Services
{
    public class OrderService(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task PlaceOrderAsync(OrderPlaceDto dto, Guid customerId)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(u => u.Id == customerId);

            if (customer is null)
                return; //error
        }
    }

}
