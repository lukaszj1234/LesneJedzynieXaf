using DevExpress.XtraPrinting.Native;

namespace LesneJedzynie1XAF.Blazor.Server.Services
{
    using System;
    using DevExpress.ExpressApp;
    using SMSApi.Api;

    namespace SmsSender
    {
        public static class Sender
        {
            public async static void WyslijSms(string wiadomosc, string nrTelefonu)
            {
                HttpClient client = new HttpClient();   
                try
                {
                    var url = $"https://api2.smsplanet.pl/sms?key=1fc48f1f-f3ff-4e90-884b-bc7e48a6a86b&password=jebacpis&from=OGRODEK&msg={wiadomosc}&to={nrTelefonu}";
                    var dupa = await client.GetAsync(url);
                }
                catch (SMSApi.Api.Exception e)
                {
                    throw;
                }
            }
        }
    }
}
