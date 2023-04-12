using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using LesneJedzynie1XAF.Blazor.Server.Services.SmsSender;
using LesneJedzynie1XAF.Module.BusinessObjects;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace LesneJedzynie1XAF.Blazor.Server.Controllers
{
    public class UzytkownikController : ObjectViewController
    {
        public UzytkownikController()
        {

            TargetObjectType= typeof(Uzytkownik);

            SimpleAction generujKluczAction = new SimpleAction(this, "GenerujkluczAction", PredefinedCategory.View)
            {
                Caption = "Generuj klucz",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
            };

            generujKluczAction.Execute += GenerujKluczAction_Execute;

            SimpleAction wyslijKluczSMS = new SimpleAction(this, "WyslijKluczSMS", PredefinedCategory.View)
            {
                Caption = "Wyślij klucz",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
                ToolTip = "Wysyła klucz do logowania poprzez SMS"
            };

            wyslijKluczSMS.Execute += WyslijKluczSMS_Execute;
        }

        private void WyslijKluczSMS_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string wiadomosc = "Twój klucz do logowania to: ";
            string bledyWiadomosc = "Coś poszło nie tak. Nie udało się wysłać wiadomości do: ";
            bool wykrytoBledy = false;
            foreach (Uzytkownik item in View.SelectedObjects)
            {
                if(!string.IsNullOrEmpty(item.Klucz) && !string.IsNullOrEmpty(item.NrTelefonu))
                {
                    try
                    {
                        Sender.WyslijSms(wiadomosc + item.Klucz, item.NrTelefonu);
                    }
                    catch (Exception f)
                    {
                        wykrytoBledy = true;
                        bledyWiadomosc += $"{item.Imie} {item.Nazwisko}; ";
                    }
                }
            }
            if(wykrytoBledy)
            {
                Application.ShowViewStrategy.ShowMessage(bledyWiadomosc, InformationType.Error, 10000, InformationPosition.Bottom);
            }
        }

        private void GenerujKluczAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (Uzytkownik item in View.SelectedObjects)
            {
                if(string.IsNullOrEmpty(item.Klucz))
                {
                    GenerujKlucz(item);
                }
            }
            ObjectSpace.CommitChanges();
        }

        void GenerujKlucz(Uzytkownik uzytkownik)
        {
            IEnumerable<char> charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charArray = charSet.Distinct().ToArray();
            char[] result = new char[8];
            for (int i = 0; i < 8; i++)
                result[i] = charArray[RandomNumberGenerator.GetInt32(charArray.Length)];
            var klucz = new string(result);
            uzytkownik.Klucz = klucz;
        }
    }
}
