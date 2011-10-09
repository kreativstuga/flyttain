using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlyttaIn.Models;
using System.Xml;

namespace FlyttaIn.Services.Communications
{
    public class CrimeServices
    {
        public const string CRIMEURL = "http://brottsplatskartan.se/api.php?action=getEvents&period={0}&area={1}";

        public List<CrimeEvent> GetCrimes(int hoursPassed, string lan)
        {
            List<CrimeEvent> retval = new List<CrimeEvent>();
            var newUrl = string.Format(CRIMEURL, hoursPassed.ToString(), lan);
            var res = Helper.CreateHttpGet(newUrl, Helper.ContentType.XML);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(res);
            var node = doc.ChildNodes[0];
            var root = doc.DocumentElement;
            var events = root.SelectNodes("Event");
            foreach (XmlNode eventObject in events)
            {
                var eventChildren = events.Cast<XmlNode>();
                var title = eventChildren.Where(x => x.Name == "title").Single();
                var link = eventChildren.Where(x => x.Name == "link").Single();
                var description = eventChildren.Where(x => x.Name == "description").Single();
                var date = eventChildren.Where(x => x.Name == "date").Single();
                var latitude = eventChildren.Where(x => x.Name == "lat").Single();
                var longitude = eventChildren.Where(x => x.Name == "lng").Single();
                var accuracy = eventChildren.Where(x => x.Name == "accuracy").Single();
                var type = eventChildren.Where(x => x.Name == "type").Single();
                var place = eventChildren.Where(x => x.Name == "place").Single();
                retval.Add(new CrimeEvent { Title = title.InnerText, Link = link.InnerText, Description = description.InnerText, Date = date.InnerText, Latitude = latitude.InnerText, Longitude = longitude.InnerText, Place = place.InnerText, Type = type.InnerText, Accuracy = accuracy.InnerText });
            }
            return retval;
        }
    }
}