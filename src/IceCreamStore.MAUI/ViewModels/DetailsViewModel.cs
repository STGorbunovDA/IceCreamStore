using CommunityToolkit.Mvvm.ComponentModel;
using IceCreamStore.Shared.Dtos;

namespace IceCreamStore.MAUI.ViewModels
{
    // detailspage?Icecream=VALUE
    [QueryProperty(nameof(Icecream), nameof(Icecream))]
    public partial class DetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private IcecreamDto? _icecream;
    }
}
