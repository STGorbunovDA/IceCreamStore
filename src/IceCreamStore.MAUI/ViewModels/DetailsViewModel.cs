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
        public DetailsViewModel(CartViewModel cartViewModel) 
        {
            _cartViewModel = cartViewModel;
        }

        [ObservableProperty]
        private IcecreamDto? _icecream;

        [ObservableProperty]
        private int _quantity;

        [ObservableProperty]
        private IcecreamOption[] _options = [];
        private readonly CartViewModel _cartViewModel;

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

            Quantity = _cartViewModel.GetItemCartCount(value.Id);
        }

        [RelayCommand]
        private void Selectoption(IcecreamOption newOption)
        {
            var newIsSelected = !newOption.IsSelected;
            // Deselect all options
            Options = [..Options.Select(o => { o.IsSelected =  false; return o; })];
            newOption.IsSelected = newIsSelected;
        }

        [RelayCommand]
        private async Task AddToCartAsync()
        {
            var selectedOption = Options.FirstOrDefault(o => o.IsSelected) ?? Options[0];
            _cartViewModel.AddItemToCart(Icecream, Quantity, selectedOption.Flavor, selectedOption.Topping);
            await GoBackAsync();
        }
    }
}
