using PodlewaczkaMobile.DTO;
using PodlewaczkaMobile.Sevices;
using PodlewaczkaMobile.ViewModels;

namespace PodlewaczkaMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var service = new OdczytSerwis();
            OdczytViewModel viewModel = new OdczytViewModel();
            try
            {
                viewModel.GetOdczytPubliczny();
            }catch(Exception e)
            {
                Shell.Current.DisplayAlert("Coś poszło nie tak", e.Message, "OK");
                return;
            }

            BindingContext = viewModel;
        }

        //async void OnOpenWebButtonClicked(System.Object sender, System.EventArgs e)
        //{
        //    await Browser.OpenAsync("https://www.devexpress.com/maui/");
        //}
    }
}