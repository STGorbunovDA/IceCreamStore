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
            var existingItem = CartItems.FirstOrDefault(ci => ci.IcecreamId == icecream.Id);
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
                    Quantity = quantity,
                    ToppingName = topping
                };

                var entity = new Data.CartItemEntity(cartItem);
                await _databaseService.AddCartItem(entity);

                cartItem.Id = entity.Id;

                CartItems.Add(cartItem);

                await ShowToastAsync("Icecream added to cart");
            }

            TotalCartCount = CartItems.Sum(i => i.Quantity);
            TotalCartCountChanged?.Invoke(null, TotalCartCount);
        }
    
        public int GetItemCartCount(int icecreamId)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.IcecreamId == icecreamId);
            return existingItem?.Quantity ?? 0;
        }

        public async Task InitializeCartAsync()
        {
            var dbItems = await _databaseService.GetAllCartItemsAsync();
            foreach (var dbItem in dbItems)
            {
                CartItems.Add(dbItem.ToCartItemModel());
            }
        }
    }
}
