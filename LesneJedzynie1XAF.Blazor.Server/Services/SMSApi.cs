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
            public static void WyslijSms(string wiadomosc, string nrTelefonu)
            {
                try
                {
                    IClient client = new ClientOAuth("1phFgj4ISIjSon03ElDIsx1d6awf95RUXDhAYfaG");

                    var smsApi = new SMSFactory(client, new ProxyHTTP("https://api.smsapi.pl/"));

                    var result =
                        smsApi.ActionSend()
                            .SetText(wiadomosc)
                            .SetTo(nrTelefonu)
                            .Execute();
                }
                catch (SMSApi.Api.Exception e)
                {
                    throw;
                }
            }
        }
    }
}
