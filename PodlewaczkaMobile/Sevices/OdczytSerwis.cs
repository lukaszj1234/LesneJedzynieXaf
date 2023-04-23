using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PodlewaczkaMobile.DTO;

namespace PodlewaczkaMobile.Sevices
{
    public class OdczytSerwis
    {
        HttpClient httpClient;

        public OdczytSerwis()
        {
            httpClient = new HttpClient();
        }

        public async Task<GetOdczytPodlewaczkaDTO> GetOdczyt()
        {
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
    }
}
