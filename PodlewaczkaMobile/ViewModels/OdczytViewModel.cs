using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PodlewaczkaMobile.Sevices;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using PodlewaczkaMobile.DTO;

namespace PodlewaczkaMobile.ViewModels
{
    public partial class OdczytViewModel : ObservableObject
    {
        OdczytSerwis _odczytSerwis;
        public OdczytViewModel(OdczytSerwis odczytSerwis)
        {
            _odczytSerwis = odczytSerwis;
        }

        [ObservableProperty]
        private GetOdczytPodlewaczkaDTO _odczyt;

        [ICommand]
        async void GetOdczyt()
        {
            Odczyt = Task.Run(() => _odczytSerwis.GetOdczyt()).Result;
        }
    }
}
