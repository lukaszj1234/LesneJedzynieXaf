using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie1XAF.Module.BusinessObjects.Podlewaczka
{
    [DefaultClassOptions]
    public class KonfiguracjaPodlewaczka : BaseObject
    {
        public KonfiguracjaPodlewaczka(Session session) : base(session){}

        double czasSpania;
        int godzinaPodlewaniaOd;
        int godzinaPodlewaniaDo;
        int liczbaCykliPodlewania;
        int dlugoscPodlewania;
        int dlugoscPrzerwy;
        double powiadomieniePoziomWody;
        double powiadomienieNapiecie;
        int minPoziomWody;
        int wilgotnoscPodlewanie;

        [XafDisplayName("Czas spania [h]")]
        public double CzasSpania
        {
            get { return czasSpania; }
            set { SetPropertyValue(nameof(CzasSpania), ref czasSpania, value); }
        }

        public int GodzinaPodlewaniaOd
        {
            get { return godzinaPodlewaniaOd; }
            set { SetPropertyValue(nameof(GodzinaPodlewaniaOd), ref godzinaPodlewaniaOd, value); }
        }

        public int GodzinaPodlewaniaDo
        {
            get { return godzinaPodlewaniaDo; }
            set { SetPropertyValue(nameof(GodzinaPodlewaniaDo), ref godzinaPodlewaniaDo, value); }
        }

        public int LiczbaCykliPodlewania
        {
            get { return liczbaCykliPodlewania; }
            set { SetPropertyValue(nameof(LiczbaCykliPodlewania), ref liczbaCykliPodlewania, value); }
        }

        [XafDisplayName("Długość podlewania [s]")]
        public int DlugoscPodlewania
        {
            get { return dlugoscPodlewania; }
            set { SetPropertyValue(nameof(DlugoscPodlewania), ref dlugoscPodlewania, value); }
        }

        [XafDisplayName("Długość przerwy w podlewaniu [s]")]
        public int DlugoscPrzerwy
        {
            get { return dlugoscPrzerwy; }
            set { SetPropertyValue(nameof(DlugoscPrzerwy), ref dlugoscPrzerwy, value); }
        }

        public double PowiadomieniePoziomWody
        {
            get { return powiadomieniePoziomWody; }
            set { SetPropertyValue(nameof(PowiadomieniePoziomWody), ref powiadomieniePoziomWody, value); }
        }

        public double PowiadomienieNapiecie
        {
            get { return powiadomienieNapiecie; }
            set { SetPropertyValue(nameof(PowiadomienieNapiecie), ref powiadomienieNapiecie, value); }
        }

        public int MinPoziomWody
        {
            get { return minPoziomWody; }
            set { SetPropertyValue(nameof(MinPoziomWody), ref minPoziomWody, value); }
        }

        public int WilgotnoscPodlewanie
        {
            get { return wilgotnoscPodlewanie; }
            set { SetPropertyValue(nameof(WilgotnoscPodlewanie), ref wilgotnoscPodlewanie, value); }
        }
    }
}
