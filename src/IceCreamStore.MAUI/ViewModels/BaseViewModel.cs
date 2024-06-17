using CommunityToolkit.Mvvm.ComponentModel;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;
    }
}
