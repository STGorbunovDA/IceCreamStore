using IceCreamStore.MAUI.ViewModels;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using UIKit;

namespace IceCreamStore.MAUI
{
    public class TabbarBadgeRenderer : ShellRenderer
    {
        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new BadgeShellTabbarAppearanceTracker();
        }

        class BadgeShellTabbarAppearanceTracker : ShellTabBarAppearanceTracker
        {
            private UITabBarItem? _cartTabbarItem;
            public override void UpdateLayout(UITabBarController controller)
            {
                base.UpdateLayout(controller);

                if (_cartTabbarItem is null)
                {
                    const int cartTabbarItemIndex = 1;

                    var cartTabbarItem = controller.TabBar.Items?[cartTabbarItemIndex];
                    UpdateBage(CartViewModel.TotalCartCount);
                    CartViewModel.TotalCartCountChanged += CartViewModel_TotalCartCountChanged;
                }
            }

            private void CartViewModel_TotalCartCountChanged(object? sender, int newCount) =>
                UpdateBage(newCount);
            

            private void UpdateBage(int count)
            {
                if (_cartTabbarItem is not null)
                {
                    if(count <= 0)
                    {
                        _cartTabbarItem.BadgeValue = null;
                    }
                    else
                    {
                        _cartTabbarItem.BadgeValue = count.ToString();
                        _cartTabbarItem.BadgeColor = Colors.Aquamarine.ToPlatform();
                    }
                }
            }

            protected override void Dispose(bool disposing)
            {
                CartViewModel.TotalCartCountChanged -= CartViewModel_TotalCartCountChanged;
                base.Dispose(disposing);
            }
        }
    }
}
