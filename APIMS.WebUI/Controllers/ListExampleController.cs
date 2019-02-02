using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIMS.Domain;

namespace APIMS.WebUI.Controllers
{
    public class ListExampleController : BaseController
    {
        // GET: ListExample
        public ActionResult Index(int id)
        {
            var columns = db.DataColumn.Where(c => c.DC_MID == id).OrderBy(c => c.DC_Order).ToList();
            SetModuleAuthority(id);
            return View(columns);
        }
    }
}