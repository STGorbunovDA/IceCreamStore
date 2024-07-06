namespace IceCreamStore.Shared.Dtos
{
    public record OrderItemDto(long Id, int IcecreamId, string Name, int Quantity, double Price, string Flavor, string Topping)
    {
        public double TotalPrice => Quantity * Price;
    }
    public record OrderDto(long Id, DateTime OrderAt, double TotalPrice);
    public record OrderPlaceDto(OrderDto Order, OrderItemDto[] Items);

}
