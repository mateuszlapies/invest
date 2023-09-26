using Mobile.App.Pages;

namespace Mobile.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSetupClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SetupScan());
        }
    }
}