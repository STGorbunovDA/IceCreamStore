namespace IceCreamStore.Shared.Dtos
{
    public record IcecreamDto(int Id, string Name, string Image, double Price, IcecreamOptionDto[] Options);
}