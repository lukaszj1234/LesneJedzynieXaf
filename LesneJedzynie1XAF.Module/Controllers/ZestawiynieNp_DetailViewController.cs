using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using LesneJedzynie1XAF.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie1XAF.Module.Controllers
{
    public class ZestawiynieNp_DetailViewController : ObjectViewController<DetailView, ZestawiynieNp>
    {
        IObjectSpace os;
        protected override void OnActivated()
        {
            base.OnActivated();
            os = Application.CreateObjectSpace();
            View.CurrentObject = ObjectSpace.CreateObject<ZestawiynieNp>();
            ViewCurrentObject.DataOd = DateTime.Now;
            ViewCurrentObject.DataDo = DateTime.Now;
            ViewCurrentObject.Nieobecnosci = os.GetObjectsQuery<Nieobecnosc>()
                .Where(x => x.Data >= ViewCurrentObject.DataOd && x.Data <= ViewCurrentObject.DataDo).ToList();
            ViewCurrentObject.LiczbaNieobecnosci = ViewCurrentObject.Nieobecnosci.Count;
            ZarejestrujEventy();
        }

        private void ZarejestrujEventy()
        {
            foreach (var item in View.Items)
            {
                if(item.Id == nameof(ZestawiynieNp.DataOd))
                {
                    var dataOdEditor = item as PropertyEditor;
                    dataOdEditor.ValueStored += DataOdEditor_ValueStored;
                }
                if (item.Id == nameof(ZestawiynieNp.DataDo))
                {
                    var dataDdEditor = item as PropertyEditor;
                    dataDdEditor.ValueStored += DataOdEditor_ValueStored;
                }
            }
        }

        private void DataOdEditor_ValueStored(object sender, EventArgs e)
        {
            ViewCurrentObject.Nieobecnosci = os.GetObjectsQuery<Nieobecnosc>()
                .Where(x => x.Data >= ViewCurrentObject.DataOd && x.Data <= ViewCurrentObject.DataDo).ToList();
            ViewCurrentObject.LiczbaNieobecnosci = ViewCurrentObject.Nieobecnosci.Count;
            View.RefreshDataSource();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            foreach (var item in View.Items)
            {
                if (item.Id == nameof(ZestawiynieNp.DataOd))
                {
                    var dataOdEditor = item as PropertyEditor;
                    dataOdEditor.ValueStored -= DataOdEditor_ValueStored;
                }
                if (item.Id == nameof(ZestawiynieNp.DataDo))
                {
                    var dataDdEditor = item as PropertyEditor;
                    dataDdEditor.ValueStored -= DataOdEditor_ValueStored;
                }
            }
        }
    }
}
