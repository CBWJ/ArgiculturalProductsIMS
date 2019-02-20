  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIMS.WebUI.Controllers
{
    public class DrugResidueController : BaseController
    {
        // GET: DrugResidue
        public ActionResult Index()
        {
            SetModuleAuthority();
            return View();
        }
    }
}