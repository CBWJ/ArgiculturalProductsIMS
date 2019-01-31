using APIMS.WebUI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace APIMS.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册全局过滤器，异常
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RegisterView();
            //日志
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
