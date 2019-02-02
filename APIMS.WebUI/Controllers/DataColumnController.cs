using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIMS.Domain;
using APIMS.WebUI.Utility;

namespace APIMS.WebUI.Controllers
{
    public class DataColumnController : BaseController
    {
        // GET: DataColumn
        public ActionResult Index(int id)
        {
            SetModuleAuthority();
            return View(id);
        }
        public ActionResult Create(int id)
        {
            CreateAcion();
            DataColumn col = new DataColumn
            {
                DC_MID = id
            };
            return View(col);
        }

        // POST: Dictionary/Create
        [HttpPost]
        public ActionResult Create(DataColumn col, FormCollection collection)
        {
            return CreateModel(col);
        }
        public ActionResult Edit(int id)
        {
            EditAcion();
            var col = db.DataColumn.Find(id);
            return View("Create", col);
        }

        // POST: Dictionary/Edit/5
        [HttpPost]
        public ActionResult Edit(DataColumn col, FormCollection collection)
        {
            return EditModel(col);
        }
        [HttpPost]
        public ActionResult Delete(List<int> idList)
        {
            return DeleteModel<Domain.DataColumn>(idList);
        }

        public ActionResult List(int mId, int page = 1, int limit = 20, string key = "")
        {
            JsonResult ret = new JsonResult();
            ret.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var columns = db.DataColumn.Where(c => c.DC_MID == mId).ToList();
            if (!string.IsNullOrWhiteSpace(key))
            {
                columns = columns.Where(r => r.DC_Code.Contains(key) || r.DC_Name.Contains(key)).ToList();
            }
            int cnt = columns.Count();
            columns = columns.OrderByDescending(m => m.DC_Code)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToList();
            try
            {
                ret.Data = JsonHelper.SerializeObject(new
                {
                    status = 0,
                    message = "",
                    total = cnt,
                    data = columns
                });
            }
            catch (Exception ex)
            {
                ret.Data = JsonHelper.SerializeObject(new
                {
                    status = 1,
                    message = "发生异常：" + ex.Message,
                    total = 0,
                    data = ""
                });
                RecordException(ex);
            }
            return ret;
        }
    }
}