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
        private IcecreamOption[] _options = [];

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

        partial void OnIcecreamChanged(IcecreamDto? value)
        {
            Options = [];
            if (value is null)
                return;

            Options = value.Options.Select(o => new IcecreamOption
            {
                Flavor = o.Flavor,
                Topping = o.Topping,
                IsSelected = false
            }).ToArray();
        }

        [RelayCommand]
        private void Selectoption(IcecreamOption newOption)
        {
            var newIsSelected = !newOption.IsSelected;
            // Deselect all options
            Options = [..Options.Select(o => { o.IsSelected =  false; return o; })];
            newOption.IsSelected = newIsSelected;
        }
    }
}
