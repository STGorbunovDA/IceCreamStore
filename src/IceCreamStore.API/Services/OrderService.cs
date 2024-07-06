using IceCreamStore.API.Data;
using IceCreamStore.API.Data.Entities;
using IceCreamStore.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IceCreamStore.API.Services
{
    public class OrderService(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto, Guid customerId)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(u => u.Id == customerId);

            if (customer is null)
                return ResultDto.Failure("Custome does not exist");

            var orderItems = dto.Items.Select(i =>
                new OrderItem
                {
                    Flavor = i.Flavor,
                    IcecreameId = i.IcecreamId,
                    Name = i.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    Topping = i.Topping,
                    TotalPrice = i.TotalPrice,
                });

            var order = new Order
            {
                CustomerId = customerId,
                CustomerAddress = customer.Address,
                CustomerEmail = customer.Email,
                CustomerName = customer.Name,
                OrderAt = DateTime.Now,
                TotalPrice = orderItems.Sum(o => o.TotalPrice),
                Items = orderItems.ToArray()
            };
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return ResultDto.Success();
            }
            catch (Exception ex)
            {
                return ResultDto.Failure(ex.Message);
            }
        }
    }

}
