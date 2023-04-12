using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using LesneJedzynie1XAF.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie1XAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty(nameof(ImieNazwisko))]
    public class Dziecko : BaseObject
    {
        public Dziecko(Session session) : base(session){ }


        Uzytkownik rodzic;
        string imie;
        string nazwisko;
        Plec plec;

        [NonPersistent]

        public string ImieNazwisko => imie + " " + nazwisko;

        [RuleRequiredField]
        public string Imie
        {
            get { return imie; }
            set { SetPropertyValue(nameof(Imie), ref imie, value); }
        }

        [RuleRequiredField]
        public string Nazwisko
        {
            get { return nazwisko; }
            set { SetPropertyValue(nameof(Nazwisko), ref nazwisko, value); }
        }

        [XafDisplayName("Płeć")]
        public Plec Plec
        {
            get { return plec; }
            set { SetPropertyValue(nameof(Plec), ref plec, value); }
        }

        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("Nieobecności")]
        public XPCollection<Nieobecnosc> Nieobecnosci
        {
            get { return GetCollection<Nieobecnosc>(nameof(Nieobecnosci)); }
        }

        [Association]
        //[Browsable(false)]
        public Uzytkownik Rodzic
        {
            get { return rodzic; }
            set { SetPropertyValue(nameof(Rodzic), ref rodzic, value); }
        }
    }
}
