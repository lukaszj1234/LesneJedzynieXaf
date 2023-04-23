using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using LesneJedzynie1XAF.Blazor.Server.DTO.LesneJedzynie;
using LesneJedzynie1XAF.Module.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using SMSApi.Api.Action;
using System.Security.Cryptography;
using System.Text.Json;

namespace LesneJedzynie1XAF.Blazor.Server.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class LesneJedzynieApiController : ControllerBase
    {
        internal INonSecuredObjectSpaceFactory _objectSpaceFactory;
        public LesneJedzynieApiController(INonSecuredObjectSpaceFactory objectSpaceFactory)
        {
            _objectSpaceFactory = objectSpaceFactory;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LogInDTO logInDTO)
        {
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<Uzytkownik>())
            {
                var rodzic = objectSpace.GetObjectsQuery<Uzytkownik>()
                     .Where(x => x.NrTelefonu == logInDTO.NrTelefonu && x.Klucz == logInDTO.Kod).FirstOrDefault();
                if (rodzic != null)
                {
                    return Ok(new GuidDTO() { Guid = rodzic.Oid });
                }
                else
                    return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetDziecka([FromBody] GuidDTO userOid)
        {
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<Uzytkownik>())
            {
                var rodzic = objectSpace.GetObjectsQuery<Uzytkownik>().Where(x => x.Oid == userOid.Guid).FirstOrDefault();
                if (rodzic != null)
                {
                    var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    DateTime utcTime = DateTime.Now.ToUniversalTime();
                    var data = TimeZoneInfo.ConvertTimeFromUtc(utcTime, polandTimeZone);
                    var dzieckaDto = new DzieckaDto();
                    dzieckaDto.Dziecka = new List<DzieckoDTO>();
                    foreach (var dziecko in rodzic.Dzieci)
                    {
                        var dzieckoDto = new DzieckoDTO()
                        { IdDziecka = dziecko.Oid, Imie = dziecko.Imie, Nazwisko = dziecko.Nazwisko, Plec = dziecko.Plec };

                        List<DateTime> obecneDaty = ZaladujDaty(data);
                        List<DateTime> nieobecnosci = ZaladujNieobecnosci(dziecko.Oid, data);
                        dzieckoDto.Dni = new List<DzienDTO>();
                        foreach (var data1 in obecneDaty)
                        {
                            if (!nieobecnosci.Any(x => x == data1))
                            {
                                dzieckoDto.Dni.Add(new DzienDTO() { Data = data1.Date, Obecny = true });
                                continue;
                            }
                            dzieckoDto.Dni.Add(new DzienDTO() { Data = data1.Date, Obecny = false });
                        }
                        dzieckaDto.Dziecka.Add(dzieckoDto);
                    }
                    return Ok(JsonSerializer.Serialize(dzieckaDto));
                }
                else
                    return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ZglosNieobecnosci([FromBody] WysylkaDTO wyslaneDane)
        {
            var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTime utcTime = DateTime.Now.ToUniversalTime();
            var data = TimeZoneInfo.ConvertTimeFromUtc(utcTime, polandTimeZone);
            Dziecko dziecko;
            using (IObjectSpace dzieckoObjectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<Dziecko>())
            {

                using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<Nieobecnosc>())
                {
                    dziecko = objectSpace.GetObjectByKey<Dziecko>(wyslaneDane.IdDziecka);
                    List<Nieobecnosc> aktualneNieobecnosci = objectSpace.GetObjectsQuery<Nieobecnosc>()
                      .Where(x => x.Data.Date >= data.Date && x.Data.Date <= data.AddDays(15).Date
                      && x.Dziecko.Oid == wyslaneDane.IdDziecka).ToList();
                    List<DateTime> dniNieobecne = wyslaneDane.Dni.Where(x => x.Obecny == false).Select(x => x.Data.Date).ToList();
                    List<DateTime> dniObecne = wyslaneDane.Dni.Where(x => x.Obecny == true).Select(x => x.Data.Date).ToList();
                    var nieobecnosciDoUsuniecia = aktualneNieobecnosci.Where(x => dniObecne.Contains(x.Data.Date));
                    foreach (var item in nieobecnosciDoUsuniecia)
                    {
                        dziecko.Nieobecnosci.Remove(item);
                        objectSpace.Delete(item);
                    }
                    var przeslaneNieobecnosci = wyslaneDane.Dni.Where(x => x.Obecny == false).ToList();

                    foreach (var item in przeslaneNieobecnosci)
                    {
                        if (aktualneNieobecnosci.Any(x => x.Data.Date == item.Data.Date))
                        {
                            continue;
                        }
                        var nieobecnosc = objectSpace.CreateObject<Nieobecnosc>();
                        nieobecnosc.Data = item.Data;
                        dziecko.Nieobecnosci.Add(nieobecnosc);
                    }
                    objectSpace.CommitChanges();
                    dzieckoObjectSpace.CommitChanges();
                }
            }
            return Ok();
        }

        private List<DateTime> ZaladujNieobecnosci(Guid oid, DateTime data)
        {
            List<DateTime> daty = new List<DateTime>();
            List<Nieobecnosc> nieobecnosci = new List<Nieobecnosc>();

            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<Nieobecnosc>())
            {
                nieobecnosci = objectSpace.GetObjectsQuery<Nieobecnosc>()
                  .Where(x => x.Data.Date >= data.Date && x.Data.Date <= data.AddDays(15).Date
                  && x.Dziecko.Oid == oid).ToList();
            }
            foreach (var item in nieobecnosci)
            {
                daty.Add(item.Data.Date);
            }
            return daty;
        }

        private List<DateTime> ZaladujDaty(DateTime data)
        {
            List<DateTime> daty = new List<DateTime>();
            List<DzienWolny> dniWolne;
            bool pominDzisiejszyDzien = true;
            if (data.Hour < 7)
            {
                pominDzisiejszyDzien = false;
            }
            using (IObjectSpace objectSpace = _objectSpaceFactory.CreateNonSecuredObjectSpace<DzienWolny>())
            {
                dniWolne = objectSpace.GetObjectsQuery<DzienWolny>()
                    .Where(x => x.DataWolnego.Date >= data.Date && x.DataWolnego.Date <= data.AddDays(15).Date).ToList();

            }
            if (pominDzisiejszyDzien)
            {
                for (int i = 0; i < 14; i++)
                {
                    var dataDoDodania = data.AddDays(i + 1);
                    if (dataDoDodania.DayOfWeek != DayOfWeek.Saturday && dataDoDodania.DayOfWeek != DayOfWeek.Sunday
                        && !dniWolne.Any(x => x.DataWolnego.Date == dataDoDodania.Date))
                    {
                        daty.Add(dataDoDodania.Date);
                    }
                }
            }
            return daty;
        }
    }
}
