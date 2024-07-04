using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Models;
using IceCreamStore.MAUI.Services;
using IceCreamStore.Shared.Dtos;
using System.Collections.ObjectModel;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        public CartViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public ObservableCollection<CartItem> CartItems { get; set; } = [];

        public static int TotalCartCount { get; set; }
        public static event EventHandler<int>? TotalCartCountChanged;

        public async void AddItemToCart(IcecreamDto icecream, int quantity, string flavor, string topping)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.Id == icecream.Id);
            if (existingItem is not null)
            {
                var dbCartItem = await _databaseService.GetCartItemAsync(existingItem.Id);
                if (quantity <= 0)
                {
                    // User wants to remove this item form cart
                    await _databaseService.DeleteCartItem(dbCartItem);
                    CartItems.Remove(existingItem);
                    await ShowToastAsync("Icecream removed form the cart");
                }
                else
                {
                    dbCartItem.Quantity = quantity;
                    await _databaseService.UpdateCartItem(dbCartItem);

                    existingItem.Quantity = quantity;
                    await ShowToastAsync("Quantity update in the cart");
                }
            }
            else
            {
                var cartItem = new CartItem
                {
                    FlavorName = flavor,
                    IcecreamId = icecream.Id,
                    Name = icecream.Name,
                    Price = icecream.Price,
                    Quantity = quantity == 0 ? 1 : quantity,
                    ToppingName = topping
                };

                var entity = new Data.CartItemEntity(cartItem);
                await _databaseService.AddCartItem(entity);

                cartItem.Id = entity.Id;

                CartItems.Add(cartItem);

                await ShowToastAsync("Icecream added to cart");
            }

            NotifyCartCountChange();
        }

        private void NotifyCartCountChange()
        {
            TotalCartCount = CartItems.Sum(i => i.Quantity);
            TotalCartCountChanged?.Invoke(null, TotalCartCount);
        }

        public int GetItemCartCount(int icecreamId)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.Id == icecreamId);
            return existingItem?.Quantity ?? 0;
        }

        public async Task InitializeCartAsync()
        {
            var dbItems = await _databaseService.GetAllCartItemsAsync();
            foreach (var dbItem in dbItems)
            {
                CartItems.Add(dbItem.ToCartItemModel());
            }
            NotifyCartCountChange();
        }

        [RelayCommand]
        private async Task ClearCartAsync()
        {
            if(CartItems.Count == 0)
            {
                await ShowAlertAsync("Empty Cart", "There are no items in th cart");
                return;
            }

            if (await ConfirmAsync("Clear Cart?", "Do you really want to clear all the items from the cart"))
            {
                await _databaseService.ClearCartAsync();
                CartItems.Clear();
                await ShowToastAsync("Cart cleared");
                NotifyCartCountChange();
            }
        }

        [RelayCommand]
        private async Task RemoveCartItemAsync(int cartItemId)
        {
            if (await ConfirmAsync("Remove item from Cart?", "Do you really want todelet this item from the cart?"))
            {
                var existingItem = CartItems.FirstOrDefault(i => i.Id == cartItemId);
                if (existingItem is null)
                    return;

                CartItems.Remove(existingItem);

                var dbCartItem = await _databaseService.GetCartItemAsync(cartItemId);
                if (dbCartItem is null)
                    return;

                await _databaseService.DeleteCartItem(dbCartItem);

                await ShowToastAsync("Icecream removed form cart");
                NotifyCartCountChange();
            }
        }
    }
}
