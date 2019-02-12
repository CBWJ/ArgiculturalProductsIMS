using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIMS.WebUI.Controllers
{
    public class IndicatorAnalysisController : BaseController
    {
        // GET: IndicatorAnalysis
        public ActionResult Index()
        {
            SetModuleAuthority();
            return View();
        }
    }
}