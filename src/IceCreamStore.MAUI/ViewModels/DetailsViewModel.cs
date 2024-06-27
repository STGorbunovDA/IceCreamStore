using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Models;
using IceCreamStore.Shared.Dtos;

namespace IceCreamStore.MAUI.ViewModels
{
    // detailspage?Icecream=VALUE
    [QueryProperty(nameof(Icecream), nameof(Icecream))]
    public partial class DetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private IcecreamDto? _icecream;

        [ObservableProperty]
        private int _quantity;

        [ObservableProperty]
        private IcecreamOption[] _option = [];

        [RelayCommand]
        private void IncreaseQuantity() => Quantity++;
        [RelayCommand]
        private void DecreaseQuantity()
        {
            if (Quantity > 0)
                Quantity--;
        }
        [RelayCommand]
        private async Task GoBackAsync() => await GoToAsync("..", animate: true);
    }
}
