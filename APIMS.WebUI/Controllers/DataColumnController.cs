using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIMS.Domain;

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
        public ActionResult Create()
        {
            CreateAcion();
            DataColumn col = new DataColumn();
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
    }
}