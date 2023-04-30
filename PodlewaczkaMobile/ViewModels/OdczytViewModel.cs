using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PodlewaczkaMobile.Sevices;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using PodlewaczkaMobile.DTO;
using PodlewaczkaMobile.Models;

namespace PodlewaczkaMobile.ViewModels
{
    public partial class OdczytViewModel : ObservableObject
    {
        [ObservableProperty]
        private Color _napiecieKolor;

        [ObservableProperty]
        private Color _poziomWodyKolor;

        [ObservableProperty]
        private Color _wilgotnoscKolor;

        [ObservableProperty]
        private Color _poziomRozpoczecieKolor;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private OdczytPodlewaczka _odczyt;


        [ICommand]
        async Task GetOdczytAsync()
        {
            IsRefreshing = true;
            OdczytSerwis odczytSerwis = new OdczytSerwis();
            GetOdczytPodlewaczkaDTO odczytDto = null;
            try
            {
                odczytDto = Task.Run(() => odczytSerwis.GetOdczyt()).Result;
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("Coś poszło nie tak", e.Message, "OK");
                IsRefreshing = false;
                return;
            }
            if (odczytDto != null)
            {
                Odczyt = OdczytSerwis.ZamienDtoNaobiekt(odczytDto);
                Ustawkolory(odczytDto);
            }
            IsRefreshing = false;
        }

        public void GetOdczytPubliczny()
        {
            OdczytSerwis odczytSerwis = new OdczytSerwis();
            GetOdczytPodlewaczkaDTO odczytDto = null;
            try
            {
                odczytDto = Task.Run(() => odczytSerwis.GetOdczyt()).Result;
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("Coś poszło nie tak", e.Message, "OK");
                return;
            }
            if (odczytDto != null)
            {
                Odczyt = OdczytSerwis.ZamienDtoNaobiekt(odczytDto);
                Ustawkolory(odczytDto);
            }
        }

        private void Ustawkolory(GetOdczytPodlewaczkaDTO odczytDto)
        {
            UstawKolorPoziomWody(odczytDto);
            UstawKolorNapieie(odczytDto);
            UstawKolorWilgotnosc(odczytDto);
            UstawKolorPoziomRozpoczecie(odczytDto);
        }

        private void UstawKolorPoziomWody(GetOdczytPodlewaczkaDTO odczytDto)
        {
            if(odczytDto.PoziomWody > 66)
            {
                PoziomWodyKolor = Colors.Green;
                return;
            }
            if (odczytDto.PoziomWody > 33 && odczytDto.PoziomWody <= 66)
            {
                PoziomWodyKolor = Colors.Yellow;
                return;
            }
            PoziomWodyKolor = Colors.Red;
        }

        private void UstawKolorNapieie(GetOdczytPodlewaczkaDTO odczytDto)
        {
            if (odczytDto.Napiecie > 13)
            {
                NapiecieKolor = Colors.Green;
                return;
            }
            if (odczytDto.Napiecie > 12 && odczytDto.Napiecie <= 13)
            {
                NapiecieKolor = Colors.Yellow;
                return;
            }
            NapiecieKolor = Colors.Red;
        }

        private void UstawKolorWilgotnosc(GetOdczytPodlewaczkaDTO odczytDto)
        {
            if (odczytDto.Wilgotnosc > 60)
            {
                WilgotnoscKolor = Colors.Green;
                return;
            }
            if (odczytDto.Wilgotnosc > 30 && odczytDto.Wilgotnosc <= 60)
            {
                WilgotnoscKolor = Colors.Yellow;
                return;
            }
            WilgotnoscKolor = Colors.Red;
        }

        private void UstawKolorPoziomRozpoczecie(GetOdczytPodlewaczkaDTO odczytDto)
        {
            if (odczytDto.PoziomWody >= odczytDto.PoziomWodyRozpoczeciePodlewania)
            {
                PoziomRozpoczecieKolor = Colors.Red;
                return;
            }
            PoziomRozpoczecieKolor = Colors.Green;
        }
    }
}
