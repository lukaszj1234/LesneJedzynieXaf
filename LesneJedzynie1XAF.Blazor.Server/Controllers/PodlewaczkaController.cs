using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using LesneJedzynie1XAF.Blazor.Server.DTO.Podlewaczka;
using LesneJedzynie1XAF.Blazor.Server.Services.SmsSender;
using LesneJedzynie1XAF.Module.BusinessObjects;
using LesneJedzynie1XAF.Module.BusinessObjects.Podlewaczka;
using LesneJedzynieApi.DTO.Podlewaczka;
using LesneJedzynieApi.WebApi.DTO.Podlewaczka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LesneJedzynie1XAF.Blazor.Server.Controllers
{
    [Route("[controller]/[Action]")]
    public class PodlewaczkaController : ControllerBase
    {
        internal INonSecuredObjectSpaceFactory _objectSpaceFactory;
        public PodlewaczkaController(INonSecuredObjectSpaceFactory objectSpaceFactory)
        {
            _objectSpaceFactory = objectSpaceFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetKonfiguracja()
        {
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<KonfiguracjaPodlewaczka>())
            {
                var konfiguracja = objectSpace.GetObjects<KonfiguracjaPodlewaczka>().FirstOrDefault();
                var konfiguracjaDTO = new KonfiguracjaDTO()
                {
                    CzasSpania = konfiguracja.CzasSpania,
                    DlugoscPodlewania = konfiguracja.DlugoscPodlewania,
                    DlugoscPrzerwy = konfiguracja.DlugoscPrzerwy,
                    GodzinaPodlewaniaDo = konfiguracja.GodzinaPodlewaniaDo,
                    GodzinaPodlewaniaOd = konfiguracja.GodzinaPodlewaniaOd,
                    LiczbaCykliPodlewania = konfiguracja.LiczbaCykliPodlewania,
                    MinPoziomWody = konfiguracja.MinPoziomWody,
                    Wilgotnosc = konfiguracja.WilgotnoscPodlewanie
                };

                return Ok(JsonSerializer.Serialize(konfiguracjaDTO));
            }
        }

        [HttpPost]
        public async void DodajOdczyt([FromBody] OdczytPodlewaczkaDTO odczytJson)
        {
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<OdczytPodlewaczka>())
            {
                var aktualnyOdczyt = objectSpace.GetObjects<OdczytPodlewaczka>().FirstOrDefault();
                if (aktualnyOdczyt == null)
                {
                    aktualnyOdczyt = objectSpace.CreateObject<OdczytPodlewaczka>();
                }

                var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                DateTime utcTime = DateTime.Now.ToUniversalTime();
                var data = TimeZoneInfo.ConvertTimeFromUtc(utcTime, polandTimeZone);

                aktualnyOdczyt.DataOdczytu = data;
                aktualnyOdczyt.PoziomWody = odczytJson.PoziomWody;
                aktualnyOdczyt.Napiecie = odczytJson.Napiecie;
                aktualnyOdczyt.Wilgotnosc = odczytJson.Wilgotnosc;

                aktualnyOdczyt.Session.CommitTransaction();
            }
        }

        [HttpPost]
        public async void DodajRozpoczeciePodlewania([FromBody] OdczytPodlewaczkaDTO odczytJson)
        {
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<OdczytPodlewaczka>())
            {
                var aktualnyOdczyt = objectSpace.GetObjects<OdczytPodlewaczka>().FirstOrDefault();

                if (aktualnyOdczyt == null)
                {
                    aktualnyOdczyt = objectSpace.CreateObject<OdczytPodlewaczka>();
                }

                var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                DateTime utcTime = DateTime.Now.ToUniversalTime();
                var data = TimeZoneInfo.ConvertTimeFromUtc(utcTime, polandTimeZone);

                aktualnyOdczyt.DataOdczytu = data;
                aktualnyOdczyt.PoziomWody = odczytJson.PoziomWody;
                aktualnyOdczyt.PoziomWodyRozpoczeciePodlewania = odczytJson.PoziomWody;
                aktualnyOdczyt.Napiecie = odczytJson.Napiecie;
                aktualnyOdczyt.Wilgotnosc = odczytJson.Wilgotnosc;
                aktualnyOdczyt.RozpoczeciePodlewania = data;

                aktualnyOdczyt.Session.CommitTransaction();
            }
            using (IObjectSpace konfiguracjaObjectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<KonfiguracjaPodlewaczka>())
            {
                var konfiguracja = konfiguracjaObjectSpace.GetObjects<KonfiguracjaPodlewaczka>().FirstOrDefault();
                if(odczytJson.PoziomWody < konfiguracja.PowiadomieniePoziomWody)
                {
                    try
                    {
                        Sender.WyslijSms($"Niski poziom wody w zbiorniku : {odczytJson.PoziomWody}%", "723284291");
                    }catch(Exception e) { }
                }
                if (odczytJson.Napiecie < konfiguracja.PowiadomienieNapiecie)
                {
                    try
                    {
                        Sender.WyslijSms($"Niskie napięcie na akumulatorze : {odczytJson.Napiecie}V", "723284291");
                    }
                    catch (Exception e) { }
                }
            }
        }

        [HttpPost]
        public async void DodajZakonczeniePodlewania([FromBody] OdczytPodlewaczkaDTO odczytJson)
        {
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<OdczytPodlewaczka>())
            {
                var aktualnyOdczyt = objectSpace.GetObjects<OdczytPodlewaczka>().FirstOrDefault();

                if (aktualnyOdczyt == null)
                {
                    aktualnyOdczyt = objectSpace.CreateObject<OdczytPodlewaczka>();
                }

                var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                DateTime utcTime = DateTime.Now.ToUniversalTime();
                var data = TimeZoneInfo.ConvertTimeFromUtc(utcTime, polandTimeZone);

                aktualnyOdczyt.DataOdczytu = data;
                aktualnyOdczyt.PoziomWody = odczytJson.PoziomWody;
                aktualnyOdczyt.Napiecie = odczytJson.Napiecie;
                aktualnyOdczyt.Wilgotnosc = odczytJson.Wilgotnosc;
                aktualnyOdczyt.ZakonczeniePodlewania = data;

                aktualnyOdczyt.Session.CommitTransaction();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOdczyt()
        {
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<OdczytPodlewaczka>())
            {
                var aktualnyOdczyt = objectSpace.GetObjects<OdczytPodlewaczka>().FirstOrDefault();
                var odczytDto = new GetOdczytPodlewaczkaDTO()
                {
                    Napiecie = aktualnyOdczyt.Napiecie,
                    PoziomWody = aktualnyOdczyt.PoziomWody,
                    Wilgotnosc = aktualnyOdczyt.Wilgotnosc,
                    DataOdczytu = aktualnyOdczyt.DataOdczytu,
                    PoziomWodyRozpoczeciePodlewania = aktualnyOdczyt.PoziomWodyRozpoczeciePodlewania,
                    RozpoczeciePodlewania = aktualnyOdczyt.RozpoczeciePodlewania,
                    ZakonczeniePodlewania = aktualnyOdczyt.ZakonczeniePodlewania
                };
                return Ok(JsonSerializer.Serialize(odczytDto));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCurretnTime()
        {
            var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTime utcTime = DateTime.Now.ToUniversalTime();
            var data = TimeZoneInfo.ConvertTimeFromUtc(utcTime, polandTimeZone);
            var dataDto = new DataDTO() { Data = data };
            return Ok(JsonSerializer.Serialize(dataDto));
        }
    }
}
