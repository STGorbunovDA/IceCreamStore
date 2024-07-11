namespace IceCreamStore.Shared.Dtos
{
    public record class ChangePasswordDto(string OldPassword, string NewPassword, string ConfirmNewPassword);
}
