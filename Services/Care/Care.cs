using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;
using System.Linq;

namespace FlyttaIn.Services.Care
{
    public class Care
    {	
        public List<Dictionary<string, string>> GetNearest(float lat, float lon)
        {
			GeoBox box = GetSurroundingBox(lat.ToString(), lon.ToString(), 2);
			
			List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
			
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") + "/vard.xml");

            XmlNode root = doc.DocumentElement;

            foreach(XmlNode pool in root.ChildNodes)
            {
                var poolList = pool.Cast<XmlNode>().ToList();
                var poolName = poolList.Where(x => x.Name == "name").Single().InnerText;
                var poolLat = Convert.ToDouble(poolList.Where(x => x.Name == "lat").Single().Value);
                var poolLon = Convert.ToDouble(poolList.Where(x => x.Name == "lon").Single().Value);
            }      

            return list;
        }
		
		public static double distance(float lat1, float lon1, float lat2, float lon2) {
			double R = 6371;
 
	        double dLat = (Math.PI / 180) * (poolLat - lat);
	        double dLon =  (Math.PI / 180) * (poolLon - lon);
	 
	        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos( (Math.PI / 180) * (poolLat)) *Math.Cos( (Math.PI / 180) * (lat)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
	        double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
	        double d = Math.Abs(R * c);
		}
    }
}
