using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel.DataAnnotations;

namespace LesneJedzynie1XAF.Module.BusinessObjects.Podlewaczka
{
    [DefaultClassOptions]
    public class OdczytPodlewaczka : BaseObject
    {
        public OdczytPodlewaczka(Session session) : base(session) { }

        DateTime dataOdczytu;
        DateTime rozpoczeciePodlewania;
        DateTime zakonczeniePodlewania;
        double napiecie;
        double poziomWody;
        double poziomWodyRozpoczeciePodlewania;
        int wilgotnosc;

        public DateTime DataOdczytu
        {
            get { return dataOdczytu; }
            set { SetPropertyValue(nameof(DataOdczytu), ref dataOdczytu, value); }
        }

        public DateTime RozpoczeciePodlewania
        {
            get { return rozpoczeciePodlewania; }
            set { SetPropertyValue(nameof(RozpoczeciePodlewania), ref rozpoczeciePodlewania, value); }
        }
        public DateTime ZakonczeniePodlewania
        {
            get { return zakonczeniePodlewania; }
            set { SetPropertyValue(nameof(ZakonczeniePodlewania), ref zakonczeniePodlewania, value); }
        }

        public double Napiecie
        {
            get { return napiecie; }
            set { SetPropertyValue(nameof(Napiecie), ref napiecie, value); }
        }

        public double PoziomWody
        {
            get { return poziomWody; }
            set { SetPropertyValue(nameof(PoziomWody), ref poziomWody, value); }
        }

        public double PoziomWodyRozpoczeciePodlewania
        {
            get { return poziomWodyRozpoczeciePodlewania; }
            set { SetPropertyValue(nameof(PoziomWodyRozpoczeciePodlewania), ref poziomWodyRozpoczeciePodlewania, value); }
        }

        public int Wilgotnosc
        {
            get { return wilgotnosc; }
            set { SetPropertyValue(nameof(Wilgotnosc), ref wilgotnosc, value); }
        }

    }
}
