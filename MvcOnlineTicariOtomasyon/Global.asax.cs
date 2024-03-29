﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcOnlineTicariOtomasyon
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //****************** Controller Bazında AUTHORIZE İşlemi ******************

            //Bu şekilde tüm sayfalarda authorize yapabilirsin.
            //Hariç tutmak istediğin sayfaya [AllowAnonymous] yazarak hariç tutabilirsin.

            GlobalFilters.Filters.Add(new AuthorizeAttribute());

            //*************************************************************************

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
