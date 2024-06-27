using CommunityToolkit.Mvvm.ComponentModel;

namespace IceCreamStore.MAUI.Models
{
    public partial class IcecreamOption : ObservableObject
    {
        public string Flavor { get; set; }

        public string Topping { get; set; }

        [ObservableProperty]
        private bool _isSelected;
    }
}
