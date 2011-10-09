using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;
using System.Linq;
using FlyttaIn.Models;

namespace FlyttaIn.Services.Carpool
{
    public class Carpool
    {
        //public Dictionary<string, string> GetNearest(float lat, float lon)
        public List<Location> GetNearest(float lat, float lon)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") + "/bilpooler.xml");

            //XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;

            double maxDistance = 0.0f;

            string name = "";

            var loc = new Location();
            

            foreach(XmlNode pool in root.ChildNodes)
            {
                var poolList = pool.Cast<XmlNode>().ToList();
                var poolName = poolList.Where(x => x.Name == "name").Single().InnerText;
                var poolLat = Convert.ToDouble(poolList.Where(x => x.Name == "lat").Single().Value);
                var poolLon = Convert.ToDouble(poolList.Where(x => x.Name == "lon").Single().Value);

                loc.Lat = poolLat.ToString();
                loc.Long = poolLon.ToString();

                double R = 6371;
 
                double dLat = (Math.PI / 180) * (poolLat - lat);
                double dLon =  (Math.PI / 180) * (poolLon - lon);
 
                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos( (Math.PI / 180) * (poolLat)) *Math.Cos( (Math.PI / 180) * (lat)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
                double d = Math.Abs(R * c);

                if (d > maxDistance) {
                    maxDistance = d;
                    name = poolName;
                }
            }

            /*Dictionary<string,string> dict = new Dictionary<string,string>();

            dict.Add("distance", maxDistance.ToString());
            dict.Add("name", name);*/

            loc.Name = name;
            loc.Distance = maxDistance.ToString();

            var list = new List<Location>();
            list.Add(loc);

            return list;
        }
    }
}
