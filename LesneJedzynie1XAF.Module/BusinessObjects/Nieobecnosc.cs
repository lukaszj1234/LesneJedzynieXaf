using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie1XAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Nieobecnosc : BaseObject
    {
        public Nieobecnosc(Session session) : base(session) { }

        DateTime data;
        Dziecko dziecko;


        public DateTime Data
        {
            get { return data; }
            set { SetPropertyValue(nameof(Data), ref data, value); }
        }

        [Association]
        [XafDisplayName("Dziecko")]
        //[Browsable(false)]
        public Dziecko Dziecko
        {
            get { return dziecko; }
            set { SetPropertyValue(nameof(Dziecko), ref dziecko, value); }
        }
    }
}
