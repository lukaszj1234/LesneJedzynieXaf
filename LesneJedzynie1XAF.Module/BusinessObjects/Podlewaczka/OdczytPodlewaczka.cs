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

        public DateTime DataOdczytu { get; set; }
        public DateTime RozpoczeciePodlewania { get; set; }
        public DateTime ZakonczeniePodlewania { get; set; }
        public double Napiecie { get; set; }
        public double PoziomWody { get; set; }
        public double PoziomWodyRozpoczeciePodlewania { get; set; }
        public int Wilgotnosc { get; set; }
    }
}
