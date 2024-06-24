using IceCreamStore.Shared.Dtos;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private IcecreamDto[] _icecreams = [];

        private bool _isInitialized;

        public async Task InitializeAsync()
        {
            if(_isInitialized) return;

            IsBusy = true;

            try
            {
                // Make Api Call to fetch Icecreams
            }
            catch (Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally 
            { 
                IsBusy = false; 
            }
        }
    }
}
