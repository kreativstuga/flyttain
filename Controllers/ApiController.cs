using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyttaIn.Services;
using FlyttaIn.Services.Carpool;
using FlyttaIn.Services.Communications;
using Newtonsoft.Json;

namespace FlyttaIn.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// http://localhost:5944/api/communications?latitude=0.2&longitude=1.2
        /// </summary>
        /// <returns></returns>
        public JsonResult Communications(float latitude, float longitude)
        {

            var stops = new Communications().GetStops(latitude, longitude);

            return Json(stops, JsonRequestBehavior.AllowGet);
            
            //return Json(new { SourceName = sourceName, Title = "Test", Content = "Some content" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNearbyStops(string longitude,string latitude)
        {
            var result = new GoteborgServices().GetGBGStopsByCoord(latitude, longitude,10);

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParkingCommuter(string longitude, string latitude)
        {
            var result = new GoteborgServices().GetParkingHandicap(latitude, longitude, "1000");
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CarPools(string longitude, string latitude)
        {
            //var test = new GoteborgServices().GetGBGLocationToCoord("Tredje Långgatan 13B");

            var car = new Carpool().GetNearest(57,11);
            

            //var stops = new Communications().GetStops(latitude, longitude);

            //return Json(stops, JsonRequestBehavior.AllowGet);

            return Json(car, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByEniro(string longitude, string latitude)
        {
             
            var result = new EniroService().GetEniro(latitude, longitude, "2000", "bank");

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Crime()
        {
            //var test = new GoteborgServices().GetGBGLocationToCoord("Tredje Långgatan 13B");

            var crimes = new CrimeServices().GetCrimes(1440, "Västra Götalands Län");

            //var stops = new Communications().GetStops(latitude, longitude);

            //return Json(stops, JsonRequestBehavior.AllowGet);

            return Json(new { Title = "Test", Content = "Some content" }, JsonRequestBehavior.AllowGet);
        }

    }
}
