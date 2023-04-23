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
            OdczytViewModel viewModel = new OdczytViewModel(service);
            try
            {
                viewModel.Odczyt = Task.Run(() => service.GetOdczyt()).Result;
                viewModel.Odczyt.PoziomWody = 10;
            }
            catch (Exception ex)
            {
                viewModel.Odczyt = new GetOdczytPodlewaczkaDTO() { DataOdczytu = DateTime.Now };
            }

            BindingContext = viewModel;
        }

        //async void OnOpenWebButtonClicked(System.Object sender, System.EventArgs e)
        //{
        //    await Browser.OpenAsync("https://www.devexpress.com/maui/");
        //}
    }
}