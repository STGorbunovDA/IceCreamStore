using IceCreamStore.MAUI.Models;
using IceCreamStore.Shared.Dtos;
using System.Collections.ObjectModel;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        public ObservableCollection<CartItem> CartItems { get; set; } = [];

        public async void AddItemToCart(IcecreamDto icecream, int quantity, string flavor, string topping)
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.IcecreamId == icecream.Id);
            if (existingItem is not null)
            {
                if (quantity <= 0)
                {
                    // User wants to remove this item form cart
                    CartItems.Remove(existingItem);
                    await ShowToastAsync("Icecream removed form the cart");
                }
                else
                {
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
                CartItems.Add(cartItem);
                await ShowToastAsync("Icecream added to cart");
            }
        }
    }
}
