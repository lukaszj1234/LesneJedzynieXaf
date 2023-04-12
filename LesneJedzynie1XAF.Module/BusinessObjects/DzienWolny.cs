using DevExpress.ClipboardSource.SpreadsheetML;
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
    public class DzienWolny : BaseObject
    {
        public DzienWolny(Session session) : base(session) { }

        DateTime dataWolnego;


        public DateTime DataWolnego
        {
            get { return dataWolnego; }
            set { SetPropertyValue(nameof(DataWolnego), ref dataWolnego, value); }
        }
    }
}
