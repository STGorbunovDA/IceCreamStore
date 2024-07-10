namespace IceCreamStore.Shared.Dtos
{
    public record OrderItemDto(long Id, int IcecreamId, string Name, int Quantity, double Price, string Flavor, string Topping)
    {
        public double TotalPrice => Quantity * Price;
    }
    public record OrderDto(long Id, DateTime OrderAt, double TotalPrice, int ItemsCount = 0)
    {
        public string ItemCountDisplay => ItemsCount + (ItemsCount > 1 ? " Items" : " Item");
    }
    public record OrderPlaceDto(OrderDto Order, OrderItemDto[] Items);

}
