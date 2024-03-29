﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FlyttaIn
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GetParkingCommuter",
                "api/getParkingCommuter",
                new { controller = "Api", action = "GetParkingCommuter" }
            );

            routes.MapRoute(
                "GetByEniro",
                "api/getByEniro",
                new { controller = "Api", action = "GetByEniro" }
            );

            routes.MapRoute(
                "GetCarPoost",
                "api/getCarPool",
                new { controller = "Api", action = "CarPools"} 
            );

            routes.MapRoute(
                "GetStops",
                "api/getStops",
                new { controller = "Api", action = "GetNearbyStops" }
            );

            routes.MapRoute(
                "GetCrimes",
                "api/getCrimes",
                new { controller = "Api", action = "Crime" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            #region Json mvc 3

            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());

            #endregion
        }
    }
}