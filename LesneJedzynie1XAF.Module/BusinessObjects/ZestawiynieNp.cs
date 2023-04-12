using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie1XAF.Module.BusinessObjects
{
    [DomainComponent]
    public class ZestawiynieNp : NonPersistentLiteObject
    {
        DateTime dataOd;
        DateTime dataDo;
        int liczbaNieobecnosci;


        [ImmediatePostData]
        public DateTime DataOd
        {
            get { return dataOd; }
            set { SetPropertyValue(ref dataOd, value); }
        }

        [ImmediatePostData]
        public DateTime DataDo
        {
            get { return dataDo; }
            set { SetPropertyValue(ref dataDo, value); }
        }

        [ModelDefault("AllowEdit", "false")]
        public int LiczbaNieobecnosci
        {
            get { return liczbaNieobecnosci; }
            set { SetPropertyValue(ref liczbaNieobecnosci, value); }
        }

        [ModelDefault("AllowEdit", "false")]
        public List<Nieobecnosc> Nieobecnosci { get; set; }
    }
}
