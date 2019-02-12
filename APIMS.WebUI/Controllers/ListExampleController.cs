using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIMS.Domain;
using System.Data;

namespace APIMS.WebUI.Controllers
{
    public class ListExampleController : BaseController
    {
        // GET: ListExample
        public ActionResult Index(int id)
        {
            var columns = db.DataColumn.Where(c => c.DC_MID == id).OrderBy(c => c.DC_Order).ToList();
            SetModuleAuthority(id);
            //获取示例数据
            var datas = db.GridData.Where(g => g.GD_MID == id).ToList();
            var rows = (from d in datas
                        select d.GD_ROW).Distinct().ToList().OrderBy(r => r);
            DataTable dt = new DataTable();
            foreach (var col in columns)
            {
                dt.Columns.Add(col.DC_Code, typeof(string));
            }
            foreach(var row in rows)
            {
                var rowData = datas.Where(d => d.GD_ROW == row).ToList();
                DataRow drNew = dt.NewRow();
                foreach(System.Data.DataColumn col in dt.Columns)
                {
                    var colData = rowData.Where(d => d.GD_Property == col.ColumnName).FirstOrDefault();
                    drNew[col.ColumnName] = colData == null ? "" : colData.GD_Value.ToString();
                }
                dt.Rows.Add(drNew);
            }
            ViewBag.GridData = dt;
            return View(columns);
        }
    }
}