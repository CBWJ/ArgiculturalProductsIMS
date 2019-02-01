using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace APIMS.WebUI.Utility
{
    public class JsonHelper
    {
        public static string SerializeObject(object obj)
        {
            var setting = new JsonSerializerSettings();
            //setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            setting.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            return JsonConvert.SerializeObject(obj, setting);
        }
    }
}