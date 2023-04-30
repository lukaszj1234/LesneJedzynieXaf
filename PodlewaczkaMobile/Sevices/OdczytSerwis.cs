using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PodlewaczkaMobile.DTO;
using PodlewaczkaMobile.Models;

namespace PodlewaczkaMobile.Sevices
{
    public class OdczytSerwis
    {
        public async Task<GetOdczytPodlewaczkaDTO> GetOdczyt()
        {
            var httpClient = new HttpClient();
            var url = "https://bernoulli-001-site1.dtempurl.com/Podlewaczka/GetOdczyt";
            HttpResponseMessage response;
            GetOdczytPodlewaczkaDTO result = null;

            response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<GetOdczytPodlewaczkaDTO>();
            }

            return result;
        }

        public static OdczytPodlewaczka ZamienDtoNaobiekt(GetOdczytPodlewaczkaDTO odczytDto)
        {
            var odczyt = new OdczytPodlewaczka()
            {
                DataOdczytu = odczytDto.DataOdczytu.ToString("dd-MM-yyyy HH:mm:ss"),
                Napiecie = odczytDto.Napiecie.ToString() + "V",
                PoziomWody = odczytDto.PoziomWody.ToString() + "%",
                PoziomWodyRozpoczeciePodlewania = odczytDto.PoziomWodyRozpoczeciePodlewania.ToString() + "%",
                RozpoczeciePodlewania = odczytDto.RozpoczeciePodlewania.ToString("dd-MM-yyyy HH:mm:ss"),
                Wilgotnosc = odczytDto.Wilgotnosc.ToString() + "%",
                ZakonczeniePodlewania = odczytDto.ZakonczeniePodlewania.ToString("dd-MM-yyyy HH:mm:ss"),
            };

            return odczyt;
        }
    }
}
