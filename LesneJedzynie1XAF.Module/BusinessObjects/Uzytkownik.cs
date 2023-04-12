using DevExpress.CodeParser;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie1XAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Rodzic")]
    [DefaultProperty(nameof(ImieNazwisko))]
    public class Uzytkownik : BaseObject
    {
        public Uzytkownik(Session session) : base(session) { }

        string nrTelefonu;
        string imie;
        string nazwisko;
        string klucz;

        [RuleRequiredField]
        public string Imie
        {
            get { return imie; }
            set { SetPropertyValue(nameof(Imie), ref imie, value); }
        }

        [NonPersistent]

        public string ImieNazwisko => imie + " " + nazwisko;

        [RuleRequiredField]
        public string Nazwisko
        {
            get { return nazwisko; }
            set { SetPropertyValue(nameof(Nazwisko), ref nazwisko, value); }
        }

        [RuleRequiredField]
        public string NrTelefonu
        {
            get { return nrTelefonu; }
            set { SetPropertyValue(nameof(NrTelefonu), ref nrTelefonu, value); }
        }

        [ModelDefault("AllowEdit", "false")]
        public string Klucz
        {
            get { return klucz; }
            set { SetPropertyValue(nameof(Klucz), ref klucz, value); }
        }

        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<Dziecko> Dzieci
        {
            get { return GetCollection<Dziecko>(nameof(Dzieci)); }
        }
    }
}
