namespace IceCreamStore.Shared.Dtos
{
    public record LoggedInUser(Guid id, string Name, string Email, string Address);
}
