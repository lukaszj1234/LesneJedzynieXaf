using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodlewaczkaMobile.Models
{
    public class OdczytPodlewaczka
    {
        public string DataOdczytu { get; set; }
        public string RozpoczeciePodlewania { get; set; }
        public string ZakonczeniePodlewania { get; set; }
        public string Napiecie { get; set; }
        public string PoziomWody { get; set; }
        public string PoziomWodyRozpoczeciePodlewania { get; set; }
        public string Wilgotnosc { get; set; }
    }
}
